namespace SearchiaSharp.Core.Models.Common
{
    public sealed class PagedInput
    {
        public int SkipCount { get; set; }
        public PagedInput(int PageNumber = 1, int PageSize = 10)
        {
            SkipCount = (PageNumber - 1) * PageSize;
        }
    }
}
