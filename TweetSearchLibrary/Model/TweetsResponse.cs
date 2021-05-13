using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TweetSearchLibrary.Model
{
    public class TweetsResponse
    {
        public IList<Tweet> data { get; set; }
        public Metadata meta { get; set; }
    }
}
