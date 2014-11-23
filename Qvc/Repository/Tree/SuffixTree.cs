using System;
using System.Collections.Generic;
using System.Linq;

namespace Qvc.Repository.Tree
{
    public class SuffixTree
    {
        private readonly Branch _trunk = new Branch("ROOT");

        public void Add(string name, Type fruit)
        {
            var pathArray = Split(name);
            for (var start = 0; start < pathArray.Count(); start++)
            {
                _trunk.Add(pathArray.Skip(start).ToList(), new Seed(name, fruit));
            }
        }

        public Type Find(string name)
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