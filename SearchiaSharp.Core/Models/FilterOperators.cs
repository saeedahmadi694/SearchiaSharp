using SearchiaSharp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchiaSharp.Core.Models
{
    public class FilterOperators : Enumeration
    {
        public static FilterOperators Equal = new FilterOperators(1, nameof(Equal).ToLowerInvariant(), ":", false);
        public static FilterOperators NotEqual = new FilterOperators(2, nameof(NotEqual).ToLowerInvariant(), "!:", false);
        public static FilterOperators Contains = new FilterOperators(3, nameof(Contains).ToLowerInvariant(), "::", false);
        public static FilterOperators NotContains = new FilterOperators(4, nameof(NotContains).ToLowerInvariant(), "!::", false);
        public static FilterOperators StartsWith = new FilterOperators(5, nameof(StartsWith).ToLowerInvariant(), "*", false);
        public static FilterOperators GreaterThan = new FilterOperators(6, nameof(GreaterThan).ToLowerInvariant(), ">", false);
        public static FilterOperators GreaterThanOrEqual = new FilterOperators(7, nameof(GreaterThanOrEqual).ToLowerInvariant(), ">=", false);
        public static FilterOperators SmallerThan = new FilterOperators(8, nameof(SmallerThan).ToLowerInvariant(), "<", false);
        public static FilterOperators SmallerThanOrEqual = new FilterOperators(9, nameof(SmallerThanOrEqual).ToLowerInvariant(), "<=", false);
        public static FilterOperators Null = new FilterOperators(12, nameof(Null).ToLowerInvariant(), "_null", false);
        public static FilterOperators NotExists = new FilterOperators(13, nameof(NotExists).ToLowerInvariant(), "_notExist", false);
        public static FilterOperators EqualOR = new FilterOperators(14, nameof(EqualOR).ToLowerInvariant(), ",", true);
        public static FilterOperators To = new FilterOperators(15, nameof(To).ToLowerInvariant(), "TO", true);
        protected FilterOperators() { }
        protected FilterOperators(int id, string name, string symbole, bool isMultiValueFilter) : base(id, name)
        {
            Symbole = symbole;
            IsMultiValueFilter = isMultiValueFilter;
        }
        public static IEnumerable<FilterOperators> List()
        {
            return new[]
        {
            Equal,
            NotEqual,
            Contains,
            NotContains,
            StartsWith,
            GreaterThan,
            GreaterThanOrEqual,
            SmallerThan,
            SmallerThanOrEqual,
            Null,
            NotExists,
            EqualOR,
            To
        };
        }

        public string Symbole { get; set; }
        public bool IsMultiValueFilter { get; set; }
        public static FilterOperators FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return state == null
                ? throw new Exception($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}")
                : state;
        }

        public static FilterOperators From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            return state == null
                ? throw new Exception($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}")
                : state;
        }
    }


}
