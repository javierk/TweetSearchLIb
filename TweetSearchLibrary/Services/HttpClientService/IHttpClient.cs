using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TweetSearchLibrary.Services.HttpClientService
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> HeadAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> GetAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> PostAsync(string url, string content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> PutAsync(string url, string content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
        Task<HttpResponseMessage> DeleteAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>));
    }
}
