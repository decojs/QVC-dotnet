using System;

namespace Qvc.Repository.Tree
{
    internal class Seed
    {
        public readonly Type Value;
        public readonly string Name;

        public Seed(string name, Type value)
        {
            Name = name;
            Value = value;
        }
    }
}