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
        
        /// <summary>
        /// Perform a Get Request
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> Get<T>(string path) where T : class
        {
            return await ClientManager.Get<T>(path);
        }
        
        /// <summary>
        /// Perform a Post request
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> Post<T>(string path) where T : class
        {
            return await Post<T>(path, "");
        }
        
        /// <summary>
        /// Perform a Post Request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bodyRaw"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> Post<T>(string path, object bodyRaw) where T : class
        {
            return await ClientManager.Post<T>(path, bodyRaw);
        }

    }
}