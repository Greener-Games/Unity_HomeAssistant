using System.Threading.Tasks;

namespace Requests
{
    public class ConfigClient : ClientBase
    {
        /// <summary>
        /// Returns the current configuration
        /// </summary>
        /// <returns>A <see cref="ConfigObject" />.</returns>
        public static async Task<ConfigObject> GetConfiguration() => await Get<ConfigObject>("api/config");
    }
}