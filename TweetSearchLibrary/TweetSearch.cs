using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSearchLibrary.Model;
using TweetSearchLibrary.Services.HttpClientService;
using TweetSearchLibrary.Services.SerializeService;

namespace TweetSearchLibrary
{
    public class TweetSearch: ITweetSearch
    {
        private readonly int _requestCap;
        private readonly int _maxResultsPerSearch;
        private int _requestCount = 0;
        private readonly string _bearerKey;
        private readonly IHttpClient _customHttpClient;
        private readonly ISerializer _customJsonSerializer;

        public TweetSearch(int requestCap, int maxResultsPerSearch, string bearer, IHttpClient httpClient, ISerializer serializer)
        {
            _requestCap = requestCap;
            _maxResultsPerSearch = maxResultsPerSearch;
            _bearerKey = bearer;
            _customHttpClient = httpClient;
            _customJsonSerializer = serializer;
        }

        public async  Task<List<Tweet>> GetAllRelevantTweets(string username, DateTime newestDate, DateTime oldestDate, string searchKeys)
        {
            var userId = await GetUserId(username);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("wrong userName provided");
            }

            string LastDateSearch = newestDate.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var url = $"https://api.twitter.com/2/users/{userId}/tweets?tweet.fields=created_at&max_results={_maxResultsPerSearch}&end_time={LastDateSearch}";

            var nextPage = url;
            DateTime lastDateOfbatch = DateTime.Now;
			List<Tweet> relevantTweets = new List<Tweet>();

			do
			{
				await _customHttpClient.GetAsync(nextPage, (new[] { new { Key = "Authorization", Value = _bearerKey } }).ToDictionary(x => x.Key, x => x.Value))
				   .ContinueWith(async (tweetSearchTask) =>
				   {
                       _requestCount++;
					   var response = await tweetSearchTask;
					   if (response.IsSuccessStatusCode)
					   {
						   string jsonString = await response.Content.ReadAsStringAsync();
						   var result = await _customJsonSerializer.DeserializeAsync<TweetsResponse>(jsonString);

						   if (result != null)
						   {
                               relevantTweets.AddRange(result.data.Where(x => searchKeys.Split(' ').ToList().Any(y => x.text.ToLower().Contains(y))).ToList());
                               lastDateOfbatch = result.data.Where(x => x.id == result.meta.oldest_id).FirstOrDefault().created_At;
							   nextPage = result.meta.next_token != null ? nextPage = url + "&pagination_token=" + result.meta.next_token : string.Empty;
						   }
					   }
					   else
					   {
							nextPage = string.Empty;
					   }
				   });

				if (_requestCount >= _requestCap || lastDateOfbatch < oldestDate)	
                    break;

			} while (!string.IsNullOrEmpty(nextPage));

			return relevantTweets;
		}

        public async Task<string> GetUserId(string username)
        {
            var url = $"https://api.twitter.com/2/users/by/username/{username}";

            var response = await _customHttpClient.GetAsync(url, (new[] { new { Key = "Authorization", Value = _bearerKey } }).ToDictionary(x => x.Key, x => x.Value));
            _requestCount++;
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var result = await _customJsonSerializer.DeserializeAsync<UserNameResponse>(jsonString);
                return result.data.id;
            }

            return string.Empty;
        }
    }
}
