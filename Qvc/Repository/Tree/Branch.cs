using System;
using System.Collections.Generic;
using System.Linq;
using Qvc.Exceptions;
using Qvc.Executables;

namespace Qvc.Repository.Tree
{
    internal class Branch<TExecutable> where TExecutable : IExecutable
    {
        private readonly IDictionary<string, Branch<TExecutable>> _branches = new Dictionary<string, Branch<TExecutable>>();
        private readonly Fruit<TExecutable> _fruit = new Fruit<TExecutable>();

        public Branch(string name)
        {
            Name = name;
        }

        public Branch(string name, IList<string> path, Seed<TExecutable> seed)
        {
            Name = name;
            Add(path, seed);
        }

        public string Name { get; private set; }

        public void Add(IList<string> path, Seed<TExecutable> seed)
        {
            var isLeaf = path.Count == 0;

            if (isLeaf)
            {
                _fruit.SetSeed(seed);
            }
            else
            {
                AddBranch(path.First(), path.Skip(1).ToList(), seed);
            }
        }

        public TExecutable FindFruit(IList<string> path)
        {
            if (!path.Any())
            {
                return _fruit.GetSeed();
            }
            var key = path.First();

            if (_branches.ContainsKey(key))
            {
                return _branches[key].FindFruit(path.Skip(1).ToList());
            }

            throw new ExecutableDoesNotExistException(key);
        }

        public void DrawTree(int depth)
        {
            for (var i = 0; i < depth; i++) Console.Write(" ");
            Console.WriteLine(Name + _fruit);
            _branches.Values.ToList().ForEach(branch => branch.DrawTree(depth + 1));
        }
        
        private void AddBranch(string name, IList<string> path, Seed<TExecutable> seed)
        {
            if (_branches.ContainsKey(name))
            {
                _branches[name].Add(path, seed);
            }
            else
            {
                _branches[name] = new Branch<TExecutable>(name, path, seed);
            }
        }
    }
}