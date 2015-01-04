using Qvc.Steps;
using Qvc.Steps.Implementations;

namespace Qvc
{
    public static class Action
    {
        public static IGetCommandStep Command(string name, string json)
        {
            return new GetExecutableStep(name, json);
        }

        public static IGetQueryStep Query(string name, string json)
        {
            return new GetExecutableStep(name, json);
        }
    }
}