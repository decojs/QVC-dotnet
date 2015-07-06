using System;
using System.Collections.Generic;

namespace Qvc
{
    public class Promise
    {
        public static Promise<T> Resolve<T>(T value)
        {
            return new Promise<T>((resolve, reject) => resolve(value));
        }

        public static Promise<T> Reject<T>(Exception exception)
        {
            return new Promise<T>((resolve, reject) => reject(exception));
        } 
    }

    public class Promise<T> : Promise
    {
        private enum State
        {
            Pending,
            Resolved,
            Rejected
        }

        private State _state = State.Pending;

        private T _value;

        private Exception _exception;

        private readonly List<Action<T>> successHandlers;

        private readonly List<Action<Exception>> errorHandlers; 

        public Promise(Action<Action<T>, Action<Exception>> creator)
        {
            successHandlers = new List<Action<T>>();
            errorHandlers = new List<Action<Exception>>();
            creator.Invoke(Resolve, Reject);
        }

        public Promise<TResult> Then<TResult>(Func<T, TResult> successHandle, Func<Exception, TResult> errorHandle)
        {
            return new Promise<TResult>((resolve, reject) => Remember(
                CreateHandler(successHandle, resolve, reject),
                CreateHandler(errorHandle, resolve, reject)));
        }

        public Promise<TResult> Then<TResult>(Func<T, TResult> handle)
        {
            return Then(handle, exception => { throw exception; });
        }

        public Promise<T> Catch(Func<Exception, T> handle)
        {
            return Then(value => value, handle);
        }

        public Promise<TResult> Then<TResult>(Func<T, Promise<TResult>> successHandle, Func<Exception, Promise<TResult>> errorHandle)
        {
            return new Promise<TResult>((resolve, reject) => Remember(
                CreateHandler(successHandle, resolve, reject),
                CreateHandler(errorHandle, resolve, reject)));
        }

        public Promise<TResult> Then<TResult>(Func<T, Promise<TResult>> handle)
        {
            return Then(handle, exception => { throw exception; });
        }

        public Promise<T> Catch(Func<Exception, Promise<T>> handle)
        {
            return Then(Promise.Resolve, handle);
        }

        public T Done()
        {
            return _value;
        }

        private static Action<TIn> CreateHandler<TIn, TResult>(
            Func<TIn, TResult> successHandle,
            Action<TResult> resolve,
            Action<Exception> reject)
        {
            return value =>
            {
                TResult result;
                try
                {
                    result = successHandle(value);
                }
                catch (Exception e)
                {
                    reject(e);
                    return;
                }
                resolve(result);
            };
        }

        private static Action<TIn> CreateHandler<TIn, TResult>(
            Func<TIn, Promise<TResult>> successHandle,
            Action<TResult> resolve,
            Action<Exception> reject)
        {
            return value =>
            {
                Promise<TResult> result;
                try
                {
                    result = successHandle(value);
                }
                catch (Exception e)
                {
                    reject(e);
                    return;
                }
                result.Then(resolve);
            };
        }

        private void Then(Action<T> handle)
        {
            Remember(handle);
        }

        private void Reject(Exception exception)
        {
            if (_state != State.Pending)
            {
                return;
            }

            _state = State.Rejected;
            _exception = exception;
        }

        private void Resolve(T value)
        {
            if (_state != State.Pending)
            {
                return;
            }

            _state = State.Resolved;
            _value = value;
            successHandlers.ForEach(x => x.Invoke(value));
        }

        private void Remember(Action<T> toDo)
        {
            switch (_state)
            {
                case State.Resolved:
                    toDo(_value);
                    break;
                case State.Rejected:
                    break;
                default:
                    successHandlers.Add(toDo);
                    break;
            }
        }

        private void Remember(Action<T> toDo, Action<Exception> toCatch)
        {
            switch (_state)
            {
                case State.Resolved:
                    toDo(_value);
                    break;

                case State.Rejected:
                    toCatch(_exception);
                    break;
                default:
                    successHandlers.Add(toDo);
                    errorHandlers.Add(toCatch);
                    break;
            }
        }
    }
}