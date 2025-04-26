using SearchiaSharp.Core.InfraServices;
using SearchiaSharp.Core.Models;
using SearchiaSharp.Core.Models.Common;
using SearchiaSharp.Core.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SearchiaSharp.Core.Repositories
{
    public abstract class SearchBuilder : ISearchBuilder
    {
        protected string _andSeperator = "&";
        protected string _andOperator = " AND ";
        protected string _orOperator = " OR ";
        protected string _defaultSortOrder = "ASC";
        protected string _defaultSeperator = ",";

        private string? _query;
        private readonly List<KeyValuePair<LogicalOperators, List<string>>> _filters = new List<KeyValuePair<LogicalOperators, List<string>>>();
        private readonly List<KeyValuePair<string, List<string>>> _facetFilters = new List<KeyValuePair<string, List<string>>>();
        private readonly List<string> _facets = new List<string>();
        private readonly List<string> _sorts = new List<string>();
        private readonly List<string> _fields = new List<string>();
        private string? _userId;
        private int? _from;
        private int? _size;

        public ISearchBuilder AppendQuery(string query)
        {
            _query = query;
            return this;
        }

        public ISearchBuilder AppendAvailableInBazar(bool? isAvailable)
        {
            if (isAvailable.HasValue)
            {
                AppendFilter("isAvailable", isAvailable.Value.ToString().ToLowerInvariant(), FilterOperators.Equal);
            }
            return this;
        }

        public ISearchBuilder AppendFacetFilter(List<FacetFilterDto> facetFilters)
        {
            facetFilters.ForEach(facetFilter => AppendFacetFilter(facetFilter.Facet, facetFilter.Values));
            return this;
        }

        public ISearchBuilder AppendFacetFilter(Facets facet, List<string> values)
        {
            values.ForEach(value => AppendFacetFilter(facet, value));
            return this;
        }

        public ISearchBuilder AppendFacetFilter(Facets facet, string value)
        {
            var existing = _facetFilters.FirstOrDefault(f => f.Key == facet.FiledName);

            if (existing.Key != null)
            {
                if (!existing.Value.Contains(value))
                {
                    existing.Value.Add(value);
                }
            }
            else
            {
                _facetFilters.Add(new KeyValuePair<string, List<string>>(facet.FiledName, new List<string> { value }));
            }

            return this;
        }


        public ISearchBuilder AppendFacets(List<Facets> facets)
        {
            facets.ForEach(r => AppendFacets(r));
            return this;
        }

        public ISearchBuilder AppendFacets(Facets facet)
        {
            _facets.Add(facet.FiledName);
            return this;
        }

        public ISearchBuilder AppendFieldList(List<string> fieldNames)
        {
            _fields.AddRange(fieldNames);
            return this;
        }

        public ISearchBuilder AppendFieldList<T>(Expression<Func<T, object>> fieldNames)
        {
            var items = fieldNames.ExtractFieldNames();
            AppendFieldList(items);
            return this;
        }

        public ISearchBuilder AppendFilter(string fieldName, string fieldValue, FilterOperators opt)
        {
            _filters.Add(new(LogicalOperators.And, new List<string> { $"{fieldName.ToCamelCase(".")}{opt.Symbole}{fieldValue}" }));
            return this;
        }

        public ISearchBuilder AppendFilter<T>(Expression<Func<T, object>> fieldName, string fieldValue, FilterOperators opt)
        {
            var name = fieldName.ExtractFieldNames().FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(name))
            {
                AppendFilter(name, fieldValue, opt);
            }
            return this;
        }


        public ISearchBuilder AppendFilter(string fieldName, List<string> fieldValues, FilterOperators opt, LogicalOperators logicalOperators)
        {
            var values = fieldValues
                .Select(value => $"{fieldName.ToCamelCase(".")}{opt.Symbole}{value}")
                .ToList();
            _filters.Add(new(logicalOperators, values));
            return this;
        }

        public ISearchBuilder AppendFilter<T>(Expression<Func<T, object>> fieldName, List<string> fieldValues, FilterOperators opt, LogicalOperators logicalOperators)
        {
            var name = fieldName.ExtractFieldNames().FirstOrDefault();
            var values = fieldValues
                .Select(value => $"{name}{opt.Symbole}{value}")
                .ToList();
            _filters.Add(new(logicalOperators, values));
            return this;
        }

        public ISearchBuilder AppendOffsetPaging(int from, int limit)
        {
            _from = from;
            _size = limit;
            return this;
        }

        public ISearchBuilder AppendPagePaging(int pageNumber = 1, int pageSize = 10)
        {
            _size = pageSize;
            _from = (pageNumber - 1) * pageSize;
            return this;
        }

        public ISearchBuilder AppendSort(SortModel sortModel)
        {
            AppendSort(sortModel.Field, sortModel.OrderType, sortModel.Mode, sortModel.Filter);
            return this;
        }
        public ISearchBuilder AppendSort(string field, OrderType orderType = OrderType.DESC, SortMode? mode = null, (string key, string value)? filter = null)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                return this;
            }

            var sb = new StringBuilder();
            sb.Append($"{field}<order={orderType}");

            if (mode.HasValue)
            {
                sb.Append($"{_defaultSeperator}mode={mode.Value.ToString().ToLower()}");
            }

            if (filter.HasValue)
            {
                sb.Append($"{_defaultSeperator}filter={filter.Value.key}:{filter.Value.value}");
            }

            sb.Append('>');
            _sorts.Add(sb.ToString());

            return this;
        }

        public ISearchBuilder AppendUserId(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                _userId = userId;
            }
            return this;
        }

        public string Build()
        {
            var queryParts = new List<string>
            {
                $"query={_query}"
            };

            if (_filters.Any())
            {
                var parts = new List<string>();

                foreach (var filter in _filters)
                {
                    if (filter.Value.Count > 1)
                    {
                        var joined = string.Join($"{filter.Key.Symbol}", filter.Value);

                        parts.Add($"({joined})");
                    }
                    else if (filter.Value.Count == 1)
                    {
                        parts.Add(filter.Value[0]);
                    }
                }

                if (parts.Any())
                {
                    queryParts.Add($"filters={string.Join(LogicalOperators.And.Symbol, parts)}");
                }
            }




            if (_facets.Any())
            {
                queryParts.Add($"facets={string.Join(_defaultSeperator, _facets)}");
            }

            if (_facetFilters.Any())
            {
                var filterStrings = _facetFilters
                     .SelectMany(facet => facet.Value.Select(value => $"{facet.Key}{FilterOperators.Equal.Symbole}{value}"));

                var result = string.Join(",", filterStrings);
                queryParts.Add($"facetFilters={result}");
            }

            if (_fields.Any())
            {
                queryParts.Add($"fl={string.Join(_defaultSeperator, _fields)}");
            }

            if (_sorts.Any())
            {
                queryParts.Add($"sorts={string.Join(_defaultSeperator, _sorts)}");
            }

            if (!string.IsNullOrWhiteSpace(_userId))
            {
                queryParts.Add($"userToken={_userId}");
            }

            if (_size.HasValue)
            {
                queryParts.Add($"size={_size}");
            }

            if (_from.HasValue)
            {
                queryParts.Add($"from={_from}");
            }

            return "?" + string.Join(_andSeperator, queryParts);
        }

        public void Clear()
        {
            _query = null;
            _filters.Clear();
            _facetFilters.Clear();
            _facets.Clear();
            _fields.Clear();
            _sorts.Clear();
            _userId = null;
            _from = null;
            _size = null;
        }
    }
}
