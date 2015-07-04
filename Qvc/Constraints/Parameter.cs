using System.Collections.Generic;

namespace Qvc.Constraints
{
    public class Parameter
    {
        public Parameter(string name, IReadOnlyCollection<Constraint> constraints)
        {
            Name = name;
            Constraints = constraints;
        }

        public string Name { get; private set; }
        
        public IReadOnlyCollection<Constraint> Constraints { get; private set; }
    }
}