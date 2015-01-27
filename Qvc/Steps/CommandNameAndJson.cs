namespace Qvc.Steps
{
    public class CommandNameAndJson
    {
        public string Name { get; private set; }

        public string Json { get; private set; }

        public CommandNameAndJson(string name, string json)
        {
            Name = name;
            Json = json;
        }
    }
}