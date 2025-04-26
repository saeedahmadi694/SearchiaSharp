using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchiaSharp.Core.Utilities
{
    public interface IHttpRequest
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null);
        Task DeleteAsync(string url, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(string url, object model, Dictionary<string, string> headers = null);
        Task PostAsync(string url, object model, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T>(string url, object model, Dictionary<string, string> headers = null);
        Task<string> PutAsync(string url, object model, Dictionary<string, string> headers = null);
    }
}

