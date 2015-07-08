using System;
using System.Reflection;
using System.Threading.Tasks;

using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Validation;

namespace Qvc
{
    public static class Action
    {
        public static Task<CommandNameAndJson> Command(string name, string json)
        {
            return Task.FromResult(new CommandNameAndJson(name, json));
        }

        public static Task<QueryNameAndJson> Query(string name, string json)
        {
            return Task.FromResult(new QueryNameAndJson(name, json));
        }

        public static Task<string> Constraints(string name)
        {
            return Task.FromResult(name);
        }
    }
}