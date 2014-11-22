using System;
using System.Collections.Generic;
using System.Linq;
using Qvc.Exceptions;

namespace Qvc.Repository.Tree
{
    public class Branch<T> where T : class
    {
        private readonly IDictionary<string, Branch<T>> _branches = new Dictionary<string, Branch<T>>();
        private readonly Fruit _fruit = new Fruit();

        public Branch(string name)
        {
            Name = name;
        }

        public Branch(string name, IList<string> path, Seed<T> seed)
        {
            Name = name;
            Add(path, seed);
        }

        public string Name { get; private set; }

        public void Add(IList<string> path, Seed<T> seed)
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

        public T FindFruit(IList<string> path)
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
        
        private void AddBranch(string name, IList<string> path, Seed<T> seed)
        {
            if (_branches.ContainsKey(name))
            {
                _branches[name].Add(path, seed);
            }
            else
            {
                _branches[name] = new Branch<T>(name, path, seed);
            }
        }

        public class Seed<TSeed>
        {
            public readonly TSeed Value;
            public readonly string Name;

            public Seed(string name, TSeed value)
            {
                Name = name;
                Value = value;
            }
        }

        public class Fruit
        {
            private string _fullName = string.Empty;
            private bool _isDuplicate;
            private T _seed;

            public void SetSeed(Seed<T> seed)
            {
                if (_seed != null)
                {
                    _isDuplicate = true;
                    SetDuplicateMessage(seed.Name);
                }
                else
                {
                    _seed = seed.Value;
                    _fullName = seed.Name;
                }
            }

            public T GetSeed()
            {
                if (_isDuplicate)
                {
                    throw new DuplicateExecutableException("Multiple Executables with that Name, use " + _fullName);
                }
                
                return _seed;
            }

            public void SetDuplicateMessage(string otherName)
            {
                if (_fullName == otherName)
                {
                    throw new DuplicateExecutableException("Cannot have two Executables with the same Name: " + _fullName);
                }
                
                _fullName += " or " + otherName;
            }

            public override string ToString()
            {
                return _seed != null ? " Fruit: " + _fullName : "";
            }
        }
    }
}