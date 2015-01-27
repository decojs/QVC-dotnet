using System;

namespace Qvc
{
    internal class Maybe<TInput, TResult>
    {
        private readonly TInput _input;
        private TResult _result;

        public Maybe(TInput input)
        {
            _input = input;
        }

        public Maybe<TInput, TResult> Case<T>(Func<T, TResult> match) where T : TInput
        {
            if (_input is T)
            {
                _result = match((T)_input);
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
        public static Maybe<TInput, TResult> Virtually<TInput, TResult>(this TInput input)
        {
            return new Maybe<TInput, TResult>(input);
        }
    }
}