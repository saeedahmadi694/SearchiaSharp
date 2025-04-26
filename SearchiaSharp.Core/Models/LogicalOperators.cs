using SearchiaSharp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchiaSharp.Core.Models
{
    public class LogicalOperators : Enumeration
    {
        public static LogicalOperators And = new LogicalOperators(1, nameof(And).ToLowerInvariant(), " AND ");
        public static LogicalOperators OR = new LogicalOperators(2, nameof(OR).ToLowerInvariant(), " OR ");

        public bool IsOR => Id == OR.Id;
        protected LogicalOperators() { }
        protected LogicalOperators(int id, string name, string symbol) : base(id, name)
        {
            Symbol = symbol;
        }
        public static IEnumerable<LogicalOperators> List()
        {
            return new[]
        {
            And,
            OR
        };
        }

        public string Symbol { get; set; }
        public static LogicalOperators FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return state == null
                ? throw new Exception($"Possible values for LogicalOperators: {string.Join(",", List().Select(s => s.Name))}")
                : state;
        }

        public static LogicalOperators From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            return state == null
                ? throw new Exception($"Possible values for LogicalOperators: {string.Join(",", List().Select(s => s.Name))}")
                : state;
        }
    }


}
