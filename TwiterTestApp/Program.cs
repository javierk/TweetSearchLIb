using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TweetSearchLibrary;
using TweetSearchLibrary.Model;
using TweetSearchLibrary.Services.HttpClientService;
using TweetSearchLibrary.Services.SerializeService;

namespace TwiterTestApp
{
    class Program
    {
		static async Task Main(string[] args)
		{
			const int MAX_NUMBER_REQUEST = 800;
			const string BEARER_KEY = "Bearer {replace with you bearer token from your tweeter dev account}";
			const int MAX_RESULTS_PER_REQUEST = 99;

			//parameters
			var NewestDate = DateTime.Parse("2021-05-14T07:00:33Z");
			var OldestDate = DateTime.Parse("2020-12-28T07:00:33Z");
			var searchKeys = "doge bitcoin";
			var username = "elonmusk";
		
			var httpClient = new CustomHttpClient();
			var serializer = new CustomJsonSerializer();
			var tweeterSearch = new TweetSearch(MAX_NUMBER_REQUEST, MAX_RESULTS_PER_REQUEST, BEARER_KEY, httpClient, serializer);

			Console.WriteLine("Process started. Please wait..");

			var final_result = await tweeterSearch.GetAllRelevantTweets(username, NewestDate, OldestDate, searchKeys);

			Console.WriteLine("------------------------------------------------------------------------");

			foreach (var tweet in final_result)
            {
                Console.WriteLine(string.Format("{0} ({1})\r\n", tweet.text, tweet.created_At));
            }
        }		
	}
}

