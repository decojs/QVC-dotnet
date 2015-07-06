using System.Collections.Generic;

using Qvc.Constraints;

namespace Qvc.Rules
{
    public class Pattern : IRule
    {
        public string Message { get; set; }

        public IEnumerable<string> Flags { get; set; }

        public string Regex { get; set; }
    }
}