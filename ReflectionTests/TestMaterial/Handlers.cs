using System;

using Qvc.Handlers;

namespace ReflectionTests.TestMaterial
{
    class CommandHandlerA : IHandleCommand<CommandA>
    {
        public void Handle(CommandA command)
        {
            throw new NotImplementedException();
        }
    }

    class QueryHandlerA : IHandleQuery<QueryA, int>
    {
        public int Handle(QueryA query)
        {
            throw new NotImplementedException();
        }
    }

    class CommandHandlerB : IHandleCommand<CommandB>
    {
        public void Handle(CommandB command)
        {
            throw new NotImplementedException();
        }
    }

    class QueryHandlerB : IHandleQuery<QueryB, int>
    {
        public int Handle(QueryB query)
        {
            throw new NotImplementedException();
        }
    }

    class MultipleHandler : 
        IHandleCommand<CommandC>,
        IHandleCommand<CommandD>,
        IHandleQuery<QueryC, int>,
        IHandleQuery<QueryD, int>
    {
        public void Handle(CommandC command)
        {
            throw new NotImplementedException();
        }

        public void Handle(CommandD command)
        {
            throw new NotImplementedException();
        }

        public int Handle(QueryC command)
        {
            throw new NotImplementedException();
        }

        public int Handle(QueryD query)
        {
            throw new NotImplementedException();
        }
    }
}