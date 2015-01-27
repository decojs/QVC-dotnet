using System;
using Qvc.Exceptions;
using Qvc.Executables;
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

        public static IFindCommandHandlerStep DeserializeCommand(this IJsonAndCommandType self, Func<string, Type, object> deserializeTheCommand)
        {
            return self.Virtually<IJsonAndCommandType, IFindCommandHandlerStep>()
                .Case<JsonAndType>(result => new FindCommandHandlerStep(deserializeTheCommand.Invoke(result.Json, result.Type) as ICommand))
                .Case<CommandErrorStep>(error => new CommandErrorStep(error.CommandResult))
                .Result();
        }

        public static IFindCommandHandlerStep DeserializeCommand(this IJsonAndCommandType self)
        {
            return DeserializeCommand(self, Default.Deserialize);
        }
    }
}