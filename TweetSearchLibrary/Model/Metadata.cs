using System;
using System.Collections.Generic;
using System.Text;

namespace TweetSearchLibrary.Model
{
    public class Metadata
    {
        public string oldest_id { get; set; }
        public string newest_id { get; set; }
        public int result_count { get; set; }
        public string next_token { get; set; }
    }
}

