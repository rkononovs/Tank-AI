
using NUnit.Framework;
using System.Collections.Generic;

namespace RGLM
{
    public class Rules
    {
        public void AddRule(Rule rule)
        {
            GetRules.Add(rule);
        }

        public List<Rule> GetRules { get; } = new List<Rule>();
    }
}
