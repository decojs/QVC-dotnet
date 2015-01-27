using System;
using System.Reflection;
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
                return new CommandResult(e);
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new CommandResult(e);
            }
        }

        public static ICommand DeserializeCommand(this IJsonAndCommandType self, Func<string, Type, object> deserializeTheCommand)
        {
            return self.Virtually<IJsonAndCommandType, ICommand>()
                .Case<JsonAndType>(result => deserializeTheCommand.Invoke(result.Json, result.Type) as ICommand)
                .Case<CommandResult>(error => error)
                .Result();
        }

        public static ICommand DeserializeCommand(this IJsonAndCommandType self)
        {
            return DeserializeCommand(self, Default.Deserialize);
        }
        
        public static ICommandAndHandlerType FindCommandHandler(this ICommand self, Func<ICommand, Type> findCommandHandler)
        {
            return self.Virtually<ICommand, ICommandAndHandlerType>()
                .Case<CommandResult>(error => error)
                .Default(command =>
                {
                    try
                    {
                        var handlerType = findCommandHandler.Invoke(command);
                        return new CommandAndHandlerType(command, handlerType);
                    }
                    catch (CommandHandlerDoesNotExistException e)
                    {
                        return new CommandResult(e);
                    }
                    catch (DuplicateCommandHandlerException e)
                    {
                        return new CommandResult(e);
                    }
                })
                .Result();
        }

        public static ICommandAndHandler CreateCommandHandler(this ICommandAndHandlerType self, Func<Type, object> createCommandHandler)
        {
            return self.Virtually<ICommandAndHandlerType, ICommandAndHandler>()
                .Case<CommandAndHandlerType>(result =>
                {
                    try
                    {
                        var handler = createCommandHandler.Invoke(result.HandlerType);
                        return new CommandAndHandler(result.Command, handler as IHandleExecutable);
                    }
                    catch (Exception e)
                    {
                        return new CommandResult(e);
                    }
                })
                .Case<CommandResult>(error => error)
                .Result();
        }

        public static ICommandAndHandler CreateCommandHandler(this ICommandAndHandlerType self)
        {
            return CreateCommandHandler(self, Default.CreateHandler);
        }

        public static CommandResult HandleCommand(this ICommandAndHandler self, Action<IHandleExecutable, ICommand> executeCommand)
        {
            return self.Virtually<ICommandAndHandler, CommandResult>()
                .Case<CommandAndHandler>(result =>
                {
                    try
                    {
                        executeCommand.Invoke(result.Handler, result.Command);
                        return new CommandResult();
                    }
                    catch (TargetInvocationException e)
                    {
                        return new CommandResult(e.GetBaseException());
                    }
                    catch (Exception e)
                    {
                        return new CommandResult(e);
                    }
                })
                .Case<CommandResult>(error => error)
                .Result();
            
        }

        public static CommandResult HandleCommand(this ICommandAndHandler self)
        {
            return HandleCommand(self, Default.HandleCommand);
        }

        public static string Serialize(this CommandResult self, Func<CommandResult, string> serializeResult)
        {
            return serializeResult.Invoke(self);
        }

        public static string Serialize(this CommandResult self)
        {
            return Serialize(self, Default.Serialize);
        }
    }
}