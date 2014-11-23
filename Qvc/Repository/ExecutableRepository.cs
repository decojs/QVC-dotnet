using System;
using System.Collections.Generic;
using System.Linq;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Repository.Tree;

namespace Qvc.Repository
{
    public class ExecutableRepository
    {
        private readonly SuffixTree _tree;

        public ExecutableRepository()
        {
            _tree = new SuffixTree();
        }

        public void AddExecutables(IEnumerable<Type> executables)
        {
            executables.ToList().ForEach(e => _tree.Add(e.FullName, e));
        }

        public Type FindExecutable(string name)
        {
            return _tree.Find(name);
        }

        public Type FindCommand(string name)
        {
            try
            {
                var result = _tree.Find(name);

                if (typeof(ICommand).IsAssignableFrom(result))
                {
                    return result;
                }
            }
            catch (ExecutableDoesNotExistException)
            {
                throw new CommandDoesNotExistException(name);
            }

            throw new CommandDoesNotExistException(name);
        }

        public Type FindQuery(string name)
        {
            try
            {
                var result = _tree.Find(name);
                if (typeof(IQuery).IsAssignableFrom(result))
                {
                    return result;
                }
            }
            catch (ExecutableDoesNotExistException)
            {
                throw new QueryDoesNotExistException(name);
            }

            throw new QueryDoesNotExistException(name);
        }
    }
}