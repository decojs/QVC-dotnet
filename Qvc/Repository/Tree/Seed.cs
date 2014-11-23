using Qvc.Executables;

namespace Qvc.Repository.Tree
{
    internal class Seed<TExecutable> where TExecutable : IExecutable
    {
        public readonly TExecutable Value;
        public readonly string Name;

        public Seed(string name, TExecutable value)
        {
            Name = name;
            Value = value;
        }
    }
}