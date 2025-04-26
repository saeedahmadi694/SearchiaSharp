using SearchiaSharp.Core.Models;
using SearchiaSharp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SearchiaSharp.Core.InfraServices
{
    public interface ISearchBuilder
    {
        string Build();
        void Clear();
        ISearchBuilder AppendQuery(string query);

        ISearchBuilder AppendOffsetPaging(int from = 0, int limit = 10);
        ISearchBuilder AppendPagePaging(int PageNumber = 1, int PageSize = 10);

        ISearchBuilder AppendUserId(string userId);

        ISearchBuilder AppendSort(string field, OrderType orderType = OrderType.DESC, SortMode? mode = null, (string key, string value)? filter = null);
        ISearchBuilder AppendSort(SortModel sortModel);

        ISearchBuilder AppendFieldList(List<string> fieldNames);
        ISearchBuilder AppendFieldList<T>(Expression<Func<T, object>> fieldNames);

        ISearchBuilder AppendFacets(List<Facets> facets);
        ISearchBuilder AppendFacets(Facets facet);

        ISearchBuilder AppendFacetFilter(List<FacetFilterDto> FacetFilters);
        ISearchBuilder AppendFacetFilter(Facets Facet, List<string> Values);
        ISearchBuilder AppendFacetFilter(Facets Facet, string Value);

        ISearchBuilder AppendFilter(string FieldName, string fieldValue, FilterOperators opt);
        ISearchBuilder AppendFilter(string FieldName, List<string> fieldValues, FilterOperators opt, LogicalOperators lopt);
        ISearchBuilder AppendFilter<T>(Expression<Func<T, object>> fieldName, string fieldValue, FilterOperators opt);
        ISearchBuilder AppendFilter<T>(Expression<Func<T, object>> fieldName, List<string> fieldValues, FilterOperators opt, LogicalOperators lopt);

        ISearchBuilder AppendAvailableInBazar(bool? isAvailable);
    }
}
