using System;
using System.Threading.Tasks;

namespace Qvc
{
    public static class Extensions
    {
        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> doFunc)
        {
            var input = await self;
            var result = doFunc(input);
            return result;
        }

        public static async Task<T> Catch<T>(this Task<T> self, Func<Exception, T> doFunc)
        {
            try
            {
                var input = await self;
                return input;
            }
            catch (Exception e)
            {
                return doFunc(e);
            }
        }

        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> doFunc)
        {
            var input = await self;
            var result = await doFunc(input);
            return result;
        }
    }
}