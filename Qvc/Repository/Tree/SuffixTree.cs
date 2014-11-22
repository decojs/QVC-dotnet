using System.Collections.Generic;
using System.Linq;

namespace Qvc.Repository.Tree
{
    public class SuffixTree<T> where T : class
    {
        private readonly Branch<T> _trunk = new Branch<T>("ROOT");

        public void Add(string name, T fruit)
        {
            var pathArray = Split(name);
            for (var start = 0; start < pathArray.Count(); start++)
            {
                _trunk.Add(pathArray.Skip(start).ToList(), new Branch<T>.Seed<T>(name, fruit));
            }
        }

        public T Find(string name)
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