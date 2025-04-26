using SearchiaSharp.Core.InfraServices;
using SearchiaSharp.Core.Models.Searchia;
using SearchiaSharp.Core.Utilities;

namespace SearchiaSharp.Core.Repositories
{
    public class SearchService : ISearchService
    {
        private readonly IHttpRequest _httpRequest;

        public SearchService(IHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }
    }
}
