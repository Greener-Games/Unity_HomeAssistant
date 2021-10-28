using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Requests
{
    public class ServiceClient : ClientBase
    {
        
        /// <summary>
        /// Retrieves a list of current services
        /// </summary>
        /// <returns>Available services grouped by domain.</returns>
        public static async Task<List<ServiceDomainObject>> GetServices() => await Get<List<ServiceDomainObject>>("/api/services");
        
        /// <summary>
        /// Calls a service on a particular domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="service"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static async Task<StateObject> CallService(string domain, string service, object body = null)
        {
            return await Post<StateObject>($"api/services/{domain}/{service}", body);
        }

        /// <summary>
        /// Calls a service using its full name
        /// </summary>
        /// <param name="service">The service name (e.g. "light.turn_on").</param>
        /// <returns></returns>
        public async Task<List<StateObject>> CallService(string service, object body = null)
        {
            var split = service.Split('.');
            return await Post<List<StateObject>>($"/api/services/{split[0]}/{split[1]}", body);
        }
    }
}