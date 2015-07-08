using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Qvc.Async
{
    public static class AsyncInvokeHelper
    {
        private static readonly MethodInfo ConvertOfTMethod =
            typeof(AsyncInvokeHelper).GetRuntimeMethods().Single(methodInfo => methodInfo.Name == "Convert");
        
        public static async Task<object> ExecuteAsync(
            MethodInfo actionMethodInfo,
            object instance,
            object[] orderedMethodArguments)
        {
            object invocationResult = null;

            // Somebody has an async function which returns null, not Task
            // Can't handle that!
            if (MethodIsAsyncVoid(actionMethodInfo))
            {
                throw new Exception(GetAsyncVoidFriendlyExceptionMessage(actionMethodInfo, instance));
            }

            try
            {
                invocationResult = actionMethodInfo.Invoke(instance, orderedMethodArguments);
            }
            catch (TargetInvocationException targetInvocationException)
            {
                // Capturing the exception and the original callstack and rethrow for external exception handlers.
                var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(targetInvocationException.InnerException);
                exceptionDispatchInfo.Throw();
            }

            return await CoerceResultToTaskAsync(
                invocationResult,
                actionMethodInfo.ReturnType);
        }

        private static string GetAsyncVoidFriendlyExceptionMessage(MethodBase actionMethodInfo, object instance)
        {
            var firstParameter = actionMethodInfo.GetParameters().First();
            var actual = string.Format("async void {0}.{1}({2} {3});", instance.GetType().FullName, actionMethodInfo.Name, firstParameter.ParameterType.Name, firstParameter.Name);
            var expected = string.Format("async Task {0}.{1}({2} {3});", instance.GetType().FullName, actionMethodInfo.Name, firstParameter.ParameterType.Name, firstParameter.Name);
            return "async method must return a Task!\n" + actual + "\nShould be\n" + expected;
        }

        private static bool MethodIsAsyncVoid(MethodInfo actionMethodInfo)
        {
            return actionMethodInfo.ReturnType == typeof(void) 
            && actionMethodInfo.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
        }

        // Method called via reflection.
        private static Task<object> Convert<T>(object taskAsObject)
        {
            var task = (Task<T>)taskAsObject;
            return CastToObject<T>(task);
        }

        // We need to CoerceResult as the object value returned from methodInfo.Invoke has to be cast to a Task<T>.
        // This is necessary to enable calling await on the returned task.
        // i.e we need to write the following var result = await (Task<ActualType>)mInfo.Invoke.
        // Returning Task<object> enables us to await on the result.
        // This method is intentionally not using async pattern to keep jit time (on cold start) to a minimum.
        private static Task<object> CoerceResultToTaskAsync(
            object result,
            Type returnType)
        {
            // If it is either a Task or Task<T>
            // must coerce the return value to Task<object>
            var resultAsTask = result as Task;
            if (resultAsTask == null)
            {
                return Task.FromResult(result);
            }

            if (returnType == typeof(Task))
            {
                ThrowIfWrappedTaskInstance(resultAsTask.GetType());
                return CastToObject(resultAsTask);
            }

            var taskValueType = GetTaskInnerTypeOrNull(returnType);

            // This will be the case for:
            // 1. Types which have derived from Task and Task<T>,
            // 2. Action methods which use dynamic keyword but return a Task or Task<T>.
            if (taskValueType == null)
            {
                throw new InvalidOperationException();
            }

            // for: public Task<T> Action()
            // constructs: return (Task<object>)Convert<T>((Task<T>)result)
            var genericMethodInfo = ConvertOfTMethod.MakeGenericMethod(taskValueType);
            var convertedResult = (Task<object>)genericMethodInfo.Invoke(null, new[] { result });
            return convertedResult;
        }

        private static void ThrowIfWrappedTaskInstance(Type actualTypeReturned)
        {
            // Throw if a method declares a return type of Task and returns an instance of Task<Task> or Task<Task<T>>
            // This most likely indicates that the developer forgot to call Unwrap() somewhere.
            if (actualTypeReturned == typeof(Task))
            {
                return;
            }

            var innerTaskType = GetTaskInnerTypeOrNull(actualTypeReturned);
            if (innerTaskType != null && typeof(Task).IsAssignableFrom(innerTaskType))
            {
                throw new InvalidOperationException();
            }
        }

        // Cast Task to Task of object
        private static async Task<object> CastToObject(Task task)
        {
            await task;
            return null;
        }

        // Cast Task of T to Task of object
        private static async Task<object> CastToObject<T>(Task<T> task)
        {
            return (object)await task;
        }

        private static Type GetTaskInnerTypeOrNull(Type type)
        {
            var genericType = ExtractGenericInterface(type, typeof(Task<>));

            if (genericType == null)
            {
                return null;
            }

            return genericType.GenericTypeArguments[0];
        }
        
        /// <summary>
        /// Determine whether <paramref name="queryType"/> is or implements a closed generic <see cref="Type"/>
        /// created from <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="queryType">The <see cref="Type"/> of interest.</param>
        /// <param name="interfaceType">The open generic <see cref="Type"/> to match. Usually an interface.</param>
        /// <returns>
        /// The closed generic <see cref="Type"/> created from <paramref name="interfaceType"/> that
        /// <paramref name="queryType"/> is or implements. <c>null</c> if the two <see cref="Type"/>s have no such
        /// relationship.
        /// </returns>
        /// <remarks>
        /// This method will return <paramref name="queryType"/> if <paramref name="interfaceType"/> is
        /// <c>typeof(KeyValuePair{,})</c>, and <paramref name="queryType"/> is
        /// <c>typeof(KeyValuePair{string, object})</c>.
        /// </remarks>
        private static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> matchesInterface =
                type => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == interfaceType;
            if (matchesInterface(queryType))
            {
                // Checked type matches (i.e. is a closed generic type created from) the open generic type.
                return queryType;
            }

            // Otherwise check all interfaces the type implements for a match.
            return queryType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(matchesInterface);
        }
    }
}