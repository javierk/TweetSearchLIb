using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TweetSearchLibrary.Services.HttpClientService
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public CustomHttpClient()
            => this._httpClient = new System.Net.Http.HttpClient();

        public async Task<HttpResponseMessage> HeadAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null)
            => await this.DoRequest(httpMethod: HttpMethod.Head, url: url, content: string.Empty, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> GetAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Get, url: url, content: string.Empty, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> PostAsync(string url, string content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Post, url: url, content: content, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Post, url: url, content: content, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> PutAsync(string url, string content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Put, url: url, content: content, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Put, url: url, content: content, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        public async Task<HttpResponseMessage> DeleteAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = default(IEnumerable<KeyValuePair<string, string>>))
            => await this.DoRequest(httpMethod: HttpMethod.Delete, url: url, content: string.Empty, headers: headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

        private async Task<HttpResponseMessage> DoRequest(HttpMethod httpMethod, string url, string content, IEnumerable<KeyValuePair<string, string>> headers)
            => await this.DoRequest(httpMethod: httpMethod, url: url, content: new StringContent(content: content, encoding: Encoding.UTF8, mediaType: "application/json"), headers: headers);

        private async Task<HttpResponseMessage> DoRequest(HttpMethod httpMethod, string url, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers)
        {
            var requestMessage = new HttpRequestMessage(method: httpMethod, requestUri: url);

            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                requestMessage.Content = content;

            foreach (var header in headers)
                requestMessage.Headers.Add(name: header.Key, value: header.Value);

            return await _httpClient.SendAsync(request: requestMessage);
        }
    }
}
