using System;
using Newtonsoft.Json;
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

        public static void HandleCommand(IHandleExecutable handler, ICommand command)
        {
            handler.GetType().GetMethod("Handle", new[] { command.GetType() }).Invoke(handler, new object[] { command });
        }

        public static object HandleQuery(IHandleExecutable handler, IQuery query)
        {
            return handler.GetType().GetMethod("Handle", new[] { query.GetType() }).Invoke(handler, new object[] { query });
        }

        public static string Serialize(object result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }
}