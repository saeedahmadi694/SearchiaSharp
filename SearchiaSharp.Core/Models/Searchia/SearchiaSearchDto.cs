using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SearchiaSharp.Core.Models.Searchia
{
    public class ResultBody<TModel> where TModel : class
    {
        [JsonPropertyName("totalHits")]
        public decimal? TotalHits { get; set; }

        [JsonPropertyName("searchTime")]
        public decimal? SearchTime { get; set; }

        [JsonPropertyName("queryId")]
        public string QueryId { get; set; }

        [JsonPropertyName("results")]
        public List<Result<TModel>> Results { get; set; }

        [JsonPropertyName("facets")]
        public List<Facet> Facets { get; set; }
    }

    public class Facet
    {
        [JsonPropertyName("indexName")]
        public string? IndexName { get; set; }

        [JsonPropertyName("facetName")]
        public string FacetName { get; set; }

        [JsonPropertyName("facetLabel")]
        public string FacetLabel { get; set; }

        [JsonPropertyName("fieldName")]
        public string FieldName { get; set; }

        [JsonPropertyName("fieldType")]
        public string FieldType { get; set; }

        [JsonPropertyName("facetRecords")]
        public List<FacetRecord> FacetRecords { get; set; }

        [JsonPropertyName("min")]
        public decimal? Min { get; set; }

        [JsonPropertyName("max")]
        public decimal? Max { get; set; }
    }

    public class FacetRecord
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        // Uncomment if needed
        // [JsonPropertyName("retrievedFields")]
        // public RetrievedFields RetrievedFields { get; set; }
    }

    public class RelationalIndicesSource
    {
    }

    public class Result<TModel> where TModel : class
    {
        [JsonPropertyName("indexName")]
        public string? IndexName { get; set; }

        [JsonPropertyName("documentId")]
        public string? DocumentId { get; set; }

        [JsonPropertyName("documentInfo")]
        public DocumentInfo? DocumentInfo { get; set; }

        [JsonPropertyName("beforePersonalizationPosition")]
        public decimal? BeforePersonalizationPosition { get; set; }

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        [JsonPropertyName("contentRankingDetails")]
        public string? ContentRankingDetails { get; set; }

        [JsonPropertyName("source")]
        public TModel Source { get; set; }

        [JsonPropertyName("highlight")]
        public string? Highlight { get; set; }

        [JsonPropertyName("subGroupDocs")]
        public string? SubGroupDocs { get; set; }
    }

    public class DocumentInfo
    {
        [JsonPropertyName("numberOfWords")]
        public int? NumberOfWords { get; set; }

        [JsonPropertyName("distance")]
        public int? Distance { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("numberOfExactMatch")]
        public int? NumberOfExactMatch { get; set; }

        [JsonPropertyName("exactMatch")]
        public bool? ExactMatch { get; set; }

        [JsonPropertyName("contentScore")]
        public double? ContentScore { get; set; }

        [JsonPropertyName("docRank")]
        public int? DocRank { get; set; }

        public DocumentInfo(
            int? numberOfWords,
            int? distance,
            int? position,
            int? numberOfExactMatch,
            bool? exactMatch,
            double? contentScore,
            int? docRank)
        {
            NumberOfWords = numberOfWords;
            Distance = distance;
            Position = position;
            NumberOfExactMatch = numberOfExactMatch;
            ExactMatch = exactMatch;
            ContentScore = contentScore;
            DocRank = docRank;
        }
    }

    public class SearchiaSearchDto<TModel> where TModel : class
    {
        [JsonPropertyName("statusType")]
        public string StatusType { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("entity")]
        public ResultBody<TModel> Entity { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}
