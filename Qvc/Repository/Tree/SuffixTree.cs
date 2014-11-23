using System.Collections.Generic;
using System.Linq;
using Qvc.Executables;

namespace Qvc.Repository.Tree
{
    public class SuffixTree<TExecutable> where TExecutable : IExecutable
    {
        private readonly Branch<TExecutable> _trunk = new Branch<TExecutable>("ROOT");

        public void Add(string name, TExecutable fruit)
        {
            var pathArray = Split(name);
            for (var start = 0; start < pathArray.Count(); start++)
            {
                _trunk.Add(pathArray.Skip(start).ToList(), new Seed<TExecutable>(name, fruit));
            }
        }

        public TExecutable Find(string name)
        {
            return _trunk.FindFruit(Split(name));
        }

        private static IList<string> Split(string name)
        {
            return name.Split('.');
        }

        public void Draw()
        {
            _trunk.DrawTree(0);
        }
    }
}