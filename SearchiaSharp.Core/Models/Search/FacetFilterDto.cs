using System.Collections.Generic;

namespace SearchiaSharp.Core.Models.Search
{
    public class FacetFilterDto
    {
        public Facets Facet { get; }
        public List<string> Values { get; }

        public FacetFilterDto(Facets facet, List<string> values)
        {
            Facet = facet;
            Values = values;
        }
    }
}
