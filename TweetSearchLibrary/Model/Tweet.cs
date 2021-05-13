using System;
using System.Collections.Generic;
using System.Text;

namespace TweetSearchLibrary.Model
{
    public class Tweet
    {
        public DateTime created_At { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }
}
