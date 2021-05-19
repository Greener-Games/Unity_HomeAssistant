using System.Collections.Generic;
using System.Threading.Tasks;

namespace Requests
{
    public class StateClient : RequestClient
    {

        /// <summary>
        /// Retrieves a list of current entities and their states.
        /// </summary>
        /// <returns>A <see cref="List{StateObject}" /> representing the current state.</returns>
        public static async Task<List<StateObject>> GetStates()
        {
            return await Get<List<StateObject>>("/api/states");
        }

        /// <summary>
        /// Returns a state object for specified entity_id
        /// </summary>
        /// <returns>A <see cref="StateObject" /> representing the current state of the requested <paramref name="entityId" />.</returns>
        public static async Task<StateObject> GetState(string entityId)
        {
            return await Get<StateObject>($"api/states/{entityId}");
        }
        
        /// <summary>
        /// Sets the state of an entity. If the entity does not exist, it will be created.
        /// </summary>
        /// <param name="entityId">The entity ID of the state to change.</param>
        /// <param name="newState">The new state value.</param>
        /// <param name="setAttributes">Optional. The attributes to set.</param>
        /// <returns>A <see cref="StateObject" /> representing the updated state of the updated <paramref name="entityId" />.</returns>
        public async Task<StateObject> SetState(string entityId, string newState, Dictionary<string, object> setAttributes = null)
        {
            return await Post<StateObject>($"/api/states/{entityId}", new {state = newState, attributes = setAttributes});
        }
    }
}