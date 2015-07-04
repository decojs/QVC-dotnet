using System.Collections.Generic;

namespace Qvc.Constraints
{
    public class Constraint
    {
        public Constraint(string name, IReadOnlyDictionary<string, object> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; private set; }

        public IReadOnlyDictionary<string, object> Attributes { get; private set; }
    }
}