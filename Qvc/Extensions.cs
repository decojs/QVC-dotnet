using System;
using System.Threading.Tasks;

namespace Qvc
{
    public static class Extensions
    {
        public static Task<TOut> Then<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> doFunc)
        {
            self.Wait();
            if (self.Status == TaskStatus.Faulted && self.Exception != null)
            {
                var result = new TaskCompletionSource<TOut>();
                result.SetException(self.Exception);
                return result.Task;
            }

            try
            {
                var result = doFunc(self.Result);
                return Task.FromResult(result);
            }
            catch (Exception e)
            {
                var result = new TaskCompletionSource<TOut>();
                result.SetException(e);
                return result.Task;
            }
        }

        public static Task<T> Catch<T>(this Task<T> self, Func<Exception, T> doFunc)
        {
            self.Wait();
            if (self.Status == TaskStatus.Faulted && self.Exception != null)
            {
                try
                {
                    var result = doFunc(self.Exception);
                    return Task.FromResult(result);
                }
                catch (Exception e)
                {
                    var result = new TaskCompletionSource<T>();
                    result.SetException(e);
                    return result.Task;
                }
            }

            return self;
        }

        public static Task<TOut> Then<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> doFunc)
        {
            self.Wait();
            if (self.Status == TaskStatus.Faulted && self.Exception != null)
            {
                var result = new TaskCompletionSource<TOut>();
                result.SetException(self.Exception);
                return result.Task;
            }

            return doFunc(self.Result);
        }
    }
}