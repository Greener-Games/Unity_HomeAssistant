using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Requests
{
    public class ServiceRequest : RequestClient
    {
        /// <summary>
        /// Retrieves a list of current services, separated into service domains.
        /// </summary>
        /// <returns>Available services grouped by domain.</returns>
        public async Task<List<ServiceDomainObject>> GetServices() => await Get<List<ServiceDomainObject>>("/api/services");
        
        /// <summary>
        /// Calls a service on a particular domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="service"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static async Task<StateObject> CallService(string domain, string service, Dictionary<string, object> body = null)
        {
            return await Post<StateObject>($"api/services/{domain}/{service}", body);
        }
    }
}