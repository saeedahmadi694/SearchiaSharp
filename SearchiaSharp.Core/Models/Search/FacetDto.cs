using System.Collections.Generic;

namespace SearchiaSharp.Core.Models.Search
{
    public class FacetDto
    {
        public string Title { get; }
        public List<KeyValuePair<string, string>> Items { get; }

        public FacetDto(string title, List<KeyValuePair<string, string>> items)
        {
            Title = title;
            Items = items;
        }

    }
}
