using BookHouse.Search.Infrastructure.Dtos.Search;
using BookHouse.Search.Infrastructure.Dtos.Searchia;
using SearchiaSharp.Core.Models;

namespace SearchiaSharp.Core.Utilities
{
    public static class SearchiaResultFacets
    {
        public static List<FacetDto> GetFacets<T>(this SearchiaSearchDto<T> result, List<Facets> facets) where T : class
        {
            var facetsList = new List<FacetDto>();
            foreach (var fac in facets)
            {
                var item = result.entity.facets
                        .Where(e => e.facetName == fac.FiledName)
                        .SelectMany(e => e.facetRecords).ToList()
                        .Select(e => new KeyValuePair<string, string>(e.label, e.count.ToString())).ToList();
                facetsList.Add(new FacetDto(fac.Name, item));
            }
            return facetsList;
        }
    }
}