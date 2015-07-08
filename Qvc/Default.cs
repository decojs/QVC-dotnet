using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qvc.Async;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.JsonCamelCase;

namespace Qvc
{
    public static class Default
    {
        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, JsonCamelCaseSettings());
        }

        public static string Serialize(object result)
        {
            return JsonConvert.SerializeObject(result, JsonCamelCaseSettings());
        }

        public static JsonSerializerSettings JsonCamelCaseSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new JsonCamelCaseResolver()
            };
        }

        public static object CreateHandler(Type queryHandler)
        {
            return Activator.CreateInstance(queryHandler);
        }

        public static async Task HandleCommand(IHandleExecutable handler, ICommand command)
        {
            await AsyncInvokeHelper.ExecuteAsync(
                Reflection.Reflection.GetHandleMethod(command.GetType(), handler.GetType()),
                handler,
                new object[] { command });
        }

        public static async Task<object> HandleQuery(IHandleExecutable handler, IQuery query)
        {
            return await AsyncInvokeHelper.ExecuteAsync(
                Reflection.Reflection.GetHandleMethod(query.GetType(), handler.GetType()),
                handler,
                new object[] { query });
        }
    }
}