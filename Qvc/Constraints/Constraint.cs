namespace Qvc.Constraints
{
    public class Constraint
    {
        public Constraint(string name, IRule attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; private set; }

        public IRule Attributes { get; private set; }
    }
}