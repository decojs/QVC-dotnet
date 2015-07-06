using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Validation;

namespace Qvc
{
    public static class Action
    {
        public static Promise<CommandNameAndJson> Command(string name, string json)
        {
            return Promise.Resolve(new CommandNameAndJson(name, json));
        }

        public static Promise<QueryNameAndJson> Query(string name, string json)
        {
            return Promise.Resolve(new QueryNameAndJson(name, json));
        }

        public static Promise<string> Constraints(string name)
        {
            return Promise.Resolve(name);
        } 

        public static Promise<JsonAndType> ThenFindCommand(this Promise<CommandNameAndJson> commandNameAndJson, Func<string, Type> getCommand)
        {
            return commandNameAndJson.Then(c => new JsonAndType(c.Json, getCommand(c.Name)));
        }

        public static Promise<ICommand> ThenDeserializeCommand(this Promise<JsonAndType> jsonAndType, Func<string, Type, object> deserializeTheCommand)
        {
            return jsonAndType.Then(self => deserializeTheCommand.Invoke(self.Json, self.Type) as ICommand);
        }

        public static Promise<ICommand> ThenDeserializeCommand(this Promise<JsonAndType> jsonAndType)
        {
            return ThenDeserializeCommand(jsonAndType, Default.Deserialize);
        }

        public static Promise<ICommand> ThenValidateCommand(this Promise<ICommand> command)
        {
            return command.Then(c =>
            {
                Validator.Validate(c);
                return c;
            });
        } 

        public static Promise<CommandAndHandlerType> ThenFindCommandHandler(this Promise<ICommand> command, Func<ICommand, Type> findCommandHandler)
        {
            return command.Then(self => new CommandAndHandlerType(self, findCommandHandler.Invoke(self)));
        }

        public static Promise<CommandAndHandler> ThenCreateCommandHandler(this Promise<CommandAndHandlerType> commandAndHandlerType, Func<Type, object> createCommandHandler)
        {
            return commandAndHandlerType.Then(self => new CommandAndHandler(self.Command, createCommandHandler.Invoke(self.HandlerType) as IHandleExecutable));
        }

        public static Promise<CommandAndHandler> ThenCreateCommandHandler(this Promise<CommandAndHandlerType> self)
        {
            return ThenCreateCommandHandler(self, Default.CreateHandler);
        }

        public static Promise<CommandResult> ThenHandleCommand(this Promise<CommandAndHandler> commandAndHandler, Action<IHandleExecutable, ICommand> executeCommand)
        {
            return commandAndHandler.Then(self =>
            {
                try
                {
                    executeCommand.Invoke(self.Handler, self.Command);
                    return new CommandResult();
                }
                catch (TargetInvocationException e)
                {
                    throw e.GetBaseException();
                }
            });
        }

        public static Promise<CommandResult> ThenHandleCommand(this Promise<CommandAndHandler> self)
        {
            return ThenHandleCommand(self, Default.HandleCommand);
        }

        public static Promise<string> ThenSerialize(this Promise<CommandResult> commandResult, Func<CommandResult, string> serializeResult)
        {
            return commandResult
                .Catch(e => CommandSteps.ExceptionToCommandResult(e))
                .Then(serializeResult);
        }

        public static Promise<string> ThenSerialize(this Promise<CommandResult> self)
        {
            return ThenSerialize(self, Default.Serialize);
        }
    }
}