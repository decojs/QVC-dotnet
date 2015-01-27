using System;
using Qvc.Exceptions;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;

namespace Qvc
{
    public static class CommandSteps
    {
        public static IDeserializeCommandStep FindCommand(this CommandNameAndJson commandNameAndJson, Func<string, Type> getCommand)
        {
            try
            {
                var type = getCommand.Invoke(commandNameAndJson.Name);
                return new DeserializeCommandStep(commandNameAndJson.Json, type);
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
    }
}