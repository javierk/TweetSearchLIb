using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TweetSearchLibrary.Model;

namespace TweetSearchLibrary
{
    public interface ITweetSearch
    {
        Task<string> GetUserId(string username);
        Task<List<Tweet>> GetAllRelevantTweets(string username, DateTime newestDate, DateTime oldestDate, string searchKeys);
    }
}
