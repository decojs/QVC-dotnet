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
    public static class CommandSteps
    {
        public static JsonAndType FindCommand(CommandNameAndJson commandNameAndJson, Func<string, Type> getCommand)
        {
            var type = getCommand.Invoke(commandNameAndJson.Name);
            return new JsonAndType(commandNameAndJson.Json, type);
        }

        public static ICommand DeserializeCommand(JsonAndType self, Func<string, Type, object> deserializeTheCommand)
        {
            return deserializeTheCommand.Invoke(self.Json, self.Type) as ICommand;
        }

        public static ICommand DeserializeCommand(JsonAndType self)
        {
            return DeserializeCommand(self, Default.Deserialize);
        }
        
        public static CommandAndHandlerType FindCommandHandler(ICommand self, Func<ICommand, Type> findCommandHandler)
        {
            var handlerType = findCommandHandler.Invoke(self);
            return new CommandAndHandlerType(self, handlerType);
        }

        public static CommandAndHandler CreateCommandHandler(CommandAndHandlerType self, Func<Type, object> createCommandHandler)
        {
            var handler = createCommandHandler.Invoke(self.HandlerType);
            return new CommandAndHandler(self.Command, handler as IHandleExecutable);
        }

        public static CommandAndHandler CreateCommandHandler(CommandAndHandlerType self)
        {
            return CreateCommandHandler(self, Default.CreateHandler);
        }

        public static CommandResult HandleCommand(CommandAndHandler self, Func<IHandleExecutable, ICommand, Task> executeCommand)
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
        }

        public static CommandResult HandleCommand(CommandAndHandler self)
        {
            return HandleCommand(self, Default.HandleCommand);
        }

        public static CommandResult ExceptionToCommandResult(Exception exception)
        {
            if (exception is ValidationException)
            {
                return new CommandResult(exception as ValidationException);
            }

            return new CommandResult(exception);
        }

        public static string Serialize(CommandResult self, Func<CommandResult, string> serializeResult)
        {
            return serializeResult.Invoke(self);
        }

        public static string Serialize(CommandResult self)
        {
            return Serialize(self, Default.Serialize);
        }
    }
}