using System;
using Qvc.Exceptions;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class GetExecutableStep : IGetCommandStep, IGetQueryStep
    {
        public string Name { get; private set; }

        public string Json { get; private set; }

        public GetExecutableStep(string name, string json)
        {
            Name = name;
            Json = json;
        }

        public IDeserializeCommandStep GetCommand(Func<string, Type> getCommand)
        {
            try
            {
                var type = getCommand.Invoke(Name);
                return new DeserializeCommandStep(Json, type);
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

        public IDeserializeQueryStep GetQuery(Func<string, Type> getQuery)
        {
            try
            {
                var type = getQuery.Invoke(Name);
                return new DeserializeQueryStep(Json, type);
            }
            catch (QueryDoesNotExistException e)
            {
                return new QueryErrorStep(new QueryResult(e));
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new QueryErrorStep(new QueryResult(e));
            }
        }
    }
}