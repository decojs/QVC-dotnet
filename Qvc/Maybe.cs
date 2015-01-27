using System;

namespace Qvc
{
    internal class Maybe<TInput, TResult> where TResult: class
    {
        private readonly TInput _input;
        private TResult _result;

        public Maybe(TInput input)
        {
            _input = input;
        }

        public Maybe<TInput, TResult> Case<T>(Func<T, TResult> match) where T : TInput
        {
            if (_input is T && _result == null)
            {
                _result = match((T)_input);
            }

            return this;
        }

        public Maybe<TInput, TResult> Default(Func<TInput, TResult> match)
        {
            if (_result == null)
            {
                _result = match(_input);
            }

            return this;
        }

        public TResult Result()
        {
            return _result;
        }
    }

    internal static class Maybe
    {
        public static Maybe<TInput, TResult> Virtually<TInput, TResult>(this TInput input) where TResult:class
        {
            return new Maybe<TInput, TResult>(input);
        }
    }
}