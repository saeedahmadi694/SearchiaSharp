using System.Collections.Generic;

namespace SearchiaSharp.Core.Models.Common
{
    public class PagedDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }

}
