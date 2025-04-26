using System;

namespace SearchiaSharp.Core.Models.Common
{
    public sealed class SortModel
    {
        public string Field { get; }
        public OrderType OrderType { get; }
        public SortMode? Mode { get; }
        public (string Key, string Value)? Filter { get; }

        public SortModel(string field, OrderType orderType = OrderType.DESC, SortMode? mode = null, (string key, string value)? filter = null)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            OrderType = orderType;
            Mode = mode;
            Filter = filter;
        }
    }
}