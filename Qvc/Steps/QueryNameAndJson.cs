namespace Qvc.Steps
{
    public class QueryNameAndJson
    {
        public string Name { get; private set; }

        public string Json { get; private set; }

        public QueryNameAndJson(string name, string json)
        {
            Name = name;
            Json = json;
        }
    }
}