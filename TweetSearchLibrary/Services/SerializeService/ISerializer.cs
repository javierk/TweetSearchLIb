using System.Threading.Tasks;

namespace TweetSearchLibrary.Services.SerializeService
{
    public interface ISerializer
    {
        Task<string> SerializeAsync(object element);
        Task<T> DeserializeAsync<T>(string element);
    }
}
