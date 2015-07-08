using System;
using System.Reflection;
using System.Threading.Tasks;

using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Validation;

namespace Qvc
{
    public static class CommandExtensions
    {
        public static Task<JsonAndType> ThenFindCommand(this Task<CommandNameAndJson> commandNameAndJson, Func<string, Type> getCommand)
        {
            return commandNameAndJson.Then(c => new JsonAndType(c.Json, getCommand(c.Name)));
        }

        public static Task<ICommand> ThenDeserializeCommand(this Task<JsonAndType> jsonAndType, Func<string, Type, object> deserializeTheCommand)
        {
            return jsonAndType.Then(self => deserializeTheCommand.Invoke(self.Json, self.Type) as ICommand);
        }

        public static Task<ICommand> ThenDeserializeCommand(this Task<JsonAndType> jsonAndType)
        {
            return ThenDeserializeCommand(jsonAndType, Default.Deserialize);
        }

        public static Task<ICommand> ThenValidateCommand(this Task<ICommand> command)
        {
            return command.Then(c =>
            {
                Validator.Validate(c);
                return c;
            });
        }

        public static Task<CommandAndHandlerType> ThenFindCommandHandler(this Task<ICommand> command, Func<ICommand, Type> findCommandHandler)
        {
            return command.Then(self => new CommandAndHandlerType(self, findCommandHandler.Invoke(self)));
        }

        public static Task<CommandAndHandler> ThenCreateCommandHandler(this Task<CommandAndHandlerType> commandAndHandlerType, Func<Type, object> createCommandHandler)
        {
            return commandAndHandlerType.Then(self => new CommandAndHandler(self.Command, createCommandHandler.Invoke(self.HandlerType) as IHandleExecutable));
        }

        public static Task<CommandAndHandler> ThenCreateCommandHandler(this Task<CommandAndHandlerType> self)
        {
            return ThenCreateCommandHandler(self, Default.CreateHandler);
        }

        public static Task<CommandResult> ThenHandleCommand(this Task<CommandAndHandler> commandAndHandler, Func<IHandleExecutable, ICommand, Task> executeCommand)
        {
            return commandAndHandler.Then(async self =>
            {
                try
                {
                    await executeCommand.Invoke(self.Handler, self.Command);
                    return new CommandResult();
                }
                catch (TargetInvocationException e)
                {
                    throw e.GetBaseException();
                }
            });
        }

        public static Task<CommandResult> ThenHandleCommand(this Task<CommandAndHandler> self)
        {
            return ThenHandleCommand(self, Default.HandleCommand);
        }

        public static Task<string> ThenSerializeResult(this Task<CommandResult> commandResult, Func<CommandResult, string> serializeResult)
        {
            return commandResult
                .Catch(CommandSteps.ExceptionToCommandResult)
                .Then(serializeResult);
        }

        public static Task<string> ThenSerializeResult(this Task<CommandResult> self)
        {
            return ThenSerializeResult(self, Default.Serialize);
        } 
    }
}