using Qvc.Steps;

namespace Qvc
{
    public static class Action
    {
        public static CommandNameAndJson Command(string name, string json)
        {
            return new CommandNameAndJson(name, json);
        }

        public static QueryNameAndJson Query(string name, string json)
        {
            return new QueryNameAndJson(name, json);
        }
    }
}