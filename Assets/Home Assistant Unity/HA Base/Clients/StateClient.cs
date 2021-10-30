using System.Collections.Generic;
using System.Threading.Tasks;

namespace Requests
{
    public class StateClient : ClientBase
    {

        /// <summary>
        /// Retrieves a list of current entities and their states, Not recomended on larger systems
        /// </summary>
        public static async Task<List<StateObject>> GetStates()
        {
            return await Get<List<StateObject>>("/api/states");
        }

        /// <summary>
        /// Returns a state object for specified entity_id
        /// </summary>
        public static async Task<StateObject> GetState(string entityId)
        {
            return await Get<StateObject>($"api/states/{entityId}");
        }
        
        /// <summary>
        /// Sets the state of an entity.
        /// NOTE: If the entity does not exist, it will be created.
        /// </summary>
        /// <param name="entityId">The entity ID of the state to change.</param>
        /// <param name="newState">The new state value.</param>
        /// <param name="attributesToSet">The attributes to set.</param>
        public async Task<StateObject> SetState(string entityId, string newState, Dictionary<string, object> attributesToSet = null)
        {
            return await Post<StateObject>($"/api/states/{entityId}", new {state = newState, attributes = attributesToSet});
        }
    }
}