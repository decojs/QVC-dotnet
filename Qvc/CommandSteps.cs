using System;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;

namespace Qvc
{
    public static class CommandSteps
    {
        public static IJsonAndCommandType FindCommand(this CommandNameAndJson commandNameAndJson, Func<string, Type> getCommand)
        {
            try
            {
                var type = getCommand.Invoke(commandNameAndJson.Name);
                return new JsonAndType(commandNameAndJson.Json, type);
            }
            catch (CommandDoesNotExistException e)
            {
                return new CommandErrorStep(new CommandResult(e));
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new CommandErrorStep(new CommandResult(e));
            }
        }

        public static ICommand DeserializeCommand(this IJsonAndCommandType self, Func<string, Type, object> deserializeTheCommand)
        {
            return self.Virtually<IJsonAndCommandType, ICommand>()
                .Case<JsonAndType>(result => deserializeTheCommand.Invoke(result.Json, result.Type) as ICommand)
                .Case<CommandErrorStep>(error => new CommandErrorStep(error.CommandResult))
                .Result();
        }

        public static ICommand DeserializeCommand(this IJsonAndCommandType self)
        {
            return DeserializeCommand(self, Default.Deserialize);
        }
        
        public static ICommandAndHandlerType FindCommandHandler(this ICommand self, Func<ICommand, Type> findCommandHandler)
        {
            return self.Virtually<ICommand, ICommandAndHandlerType>()
                .Case<CommandErrorStep>(error => new CommandErrorStep(error.CommandResult))
                .Default(command =>
                {
                    try
                    {
                        var handlerType = findCommandHandler.Invoke(command);
                        return new CommandAndHandlerType(command, handlerType);
                    }
                    catch (CommandHandlerDoesNotExistException e)
                    {
                        return new CommandErrorStep(new CommandResult(e));
                    }
                    catch (DuplicateCommandHandlerException e)
                    {
                        return new CommandErrorStep(new CommandResult(e));
                    }
                })
                .Result();
        }

        public static IExecuteCommandStep CreateCommandHandler(this ICommandAndHandlerType self, Func<Type, object> createCommandHandler)
        {
            return self.Virtually<ICommandAndHandlerType, IExecuteCommandStep>()
                .Case<CommandAndHandlerType>(result =>
                {
                    try
                    {
                        var handler = createCommandHandler.Invoke(result.HandlerType);
                        return new ExecuteCommandStep(result.Command, handler as IHandleExecutable);
                    }
                    catch (Exception e)
                    {
                        return new CommandErrorStep(new CommandResult(e));
                    }
                })
                .Case<CommandErrorStep>(error => new CommandErrorStep(error.CommandResult))
                .Result();
        }

        public static IExecuteCommandStep CreateCommandHandler(this ICommandAndHandlerType self)
        {
            return CreateCommandHandler(self, Default.CreateHandler);
        }
    }
}