using System.Threading.Tasks;

namespace Requests
{
    public abstract class ClientBase
    {
        /// <summary>
        /// Returns a message if the API is up and running.
        /// </summary>
        /// <returns>A <see cref="ConfigObject" />.</returns>
        public static async Task<bool> IsRunning()
        {
            MessageObject result = await Get<MessageObject>("api/");
            return result.Message == "API running.";
        }
        
        public static async Task<T> Get<T>(string path) where T : class
        {
            return await ClientManager.Get<T>(path);
        }
        
        public static async Task<T> Post<T>(string path) where T : class
        {
            return await Post<T>(path, "");
        }
        
        public static async Task<T> Post<T>(string path, object bodyRaw) where T : class
        {
            return await ClientManager.Post<T>(path, bodyRaw);
        }

    }
}