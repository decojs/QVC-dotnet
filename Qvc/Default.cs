using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qvc.Async;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc
{
    public static class Default
    {
        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static object CreateHandler(Type queryHandler)
        {
            return Activator.CreateInstance(queryHandler);
        }

        public static async Task HandleCommand(IHandleExecutable handler, ICommand command)
        {
            await AsyncInvokeHelper.ExecuteAsync(
                handler.GetType().GetMethod("Handle", new[] { command.GetType() }),
                handler,
                new object[] { command });
        }

        public static async Task<object> HandleQuery(IHandleExecutable handler, IQuery query)
        {
            return await AsyncInvokeHelper.ExecuteAsync(
                handler.GetType().GetMethod("Handle", new[] { query.GetType() }),
                handler,
                new object[] { query });
        }

        public static string Serialize(object result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }
}