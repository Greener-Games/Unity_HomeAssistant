using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Requests
{
    public class HistoryRequest : RequestClient
    {

        public static async Task<List<StateObject>> GetHistory(string entityId)
        {
            return await GetHistory(entityId, DateTime.Now, TimeSpan.FromDays(1), false, false);
        }
        
        public static async Task<List<StateObject>> GetHistory(string entityId, TimeSpan timeSpanToFetch, bool minimalResponse)
        {
            return await GetHistory(entityId, DateTime.Now, timeSpanToFetch, minimalResponse, false);
        }
    
        /// <summary>
        /// Returns an array of state changes in the past. Each object contains further details for the entities
        /// </summary>
        /// <returns>A <see cref="StateObject" />History of an entity<paramref name="entityId" />.</returns>
        public static async Task<List<StateObject>> GetHistory(string entityId, DateTimeOffset latestTimeStamp, TimeSpan timeSpanToFetch, bool minimalResponse, bool significatChangesOnly)
        {
            DateTimeOffset start = (latestTimeStamp - timeSpanToFetch);
            string request = $"api/history/period/{start.UtcDateTime:yyyy-MM-dd\\THH:mm:ss}";
            request += $"?filter_entity_id={entityId}";
            request += $"&end_time={latestTimeStamp.UtcDateTime:yyyy-MM-dd\\THH:mm:ss}";

            if (minimalResponse)
            {
                request += "&minimal_response";
            }

            if (significatChangesOnly)
            {
                request += "&significant_changes_only";  
            }

            try
            {
                List<StateObject> history = (await Get<List<List<StateObject>>>(request)).First();
                return history;
            }
            catch (Exception e)
            {
                Debug.Log($"unable to fetch history for {entityId}");

                return new List<StateObject>();
            }
        }
    }
}