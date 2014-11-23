using System;
using Qvc.Exceptions;

namespace Qvc.Repository.Tree
{
    internal class Fruit
    {
        private string _fullName = string.Empty;
        private bool _isDuplicate;
        private Type _seed;

        public void SetSeed(Seed seed)
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

        public Type GetSeed()
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