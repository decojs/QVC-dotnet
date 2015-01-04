﻿using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class ErrorStep : 
        IGetExecutableStep, 
        IDeserializeCommandStep, 
        IDeserializeQueryStep, 
        IFindCommandHandlerStep, 
        IFindQueryHandlerStep, 
        ICreateCommandHandlerStep, 
        ICreateQueryHandlerStep, 
        IExecuteCommandStep, 
        IExecuteQueryStep
    {
        private readonly Exception _exception;

        public ErrorStep(Exception exception)
        {
            _exception = exception;
        }

        public IDeserializeCommandStep GetCommand(Func<string, Type> getCommand)
        {
            return this;
        }

        public IDeserializeQueryStep GetQuery(Func<string, Type> getQuery)
        {
            return this;
        }

        public IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            return this;
        }

        public IFindCommandHandlerStep DeserializeCommand()
        {
            return this;
        }

        public IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            return this;
        }

        public IFindQueryHandlerStep DeserializeQuery()
        {
            return this;
        }

        public ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            return this;
        }

        public ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            return this;
        }

        public IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            return this;
        }

        public IExecuteCommandStep CreateCommandHandler()
        {
            return this;
        }

        public IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            return this;
        }

        public IExecuteQueryStep CreateQueryHandler()
        {
            return this;
        }

        public ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
        {
            return new SerializeResultStep(new CommandResult(_exception));
        }

        public ISerializeResultStep HandleCommand()
        {
            return new SerializeResultStep(new CommandResult(_exception));
        }

        public ISerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            return new SerializeResultStep(new QueryResult(_exception));
        }

        public ISerializeResultStep HandleQuery()
        {
            return new SerializeResultStep(new QueryResult(_exception));
        }
    }
}