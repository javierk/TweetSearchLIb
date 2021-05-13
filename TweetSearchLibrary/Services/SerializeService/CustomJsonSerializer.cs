using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;

namespace TweetSearchLibrary.Services.SerializeService
{
    public class CustomJsonSerializer : ISerializer
    {
        public Task<T> DeserializeAsync<T>(string element)
         => Task.FromResult(result: JsonConvert.DeserializeObject<T>(element));

        public Task<string> SerializeAsync(object element)
            => Task.FromResult(result: JsonConvert.SerializeObject(element, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ" }));
    }
}
