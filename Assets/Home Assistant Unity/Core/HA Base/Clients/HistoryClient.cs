#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

#endregion

namespace Requests
{
    public class HistoryClient : ClientBase
    {

        /// <summary>
        /// Retrieves a list of ALL historical states, Not recomended as can return huge lists of data
        /// </summary>
        public async Task<List<HistoryListObject>> GetHistory() => await Get<List<HistoryListObject>>("/api/history/period");
        
        /// <summary>
        ///  Returns the history for a entity ID for the last 24 hours
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static async Task<HistoryListObject> GetHistory(string entityId)
        {
            return await GetHistory(entityId, DateTime.Now - TimeSpan.FromDays(1), DateTime.Now, false, false);
        }


        /// <summary>
        ///     Returns the history for an entity going back <paramref name="timeSpanToFetch" /> in the past from now
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="timeSpanToFetch"></param>
        /// <param name="minimalResponse"></param>
        /// <returns></returns>
        public static async Task<HistoryListObject> GetHistory(string entityId, TimeSpan timeSpanToFetch, bool minimalResponse)
        {
            return await GetHistory(entityId, DateTime.Now - timeSpanToFetch, DateTime.Now, minimalResponse, false);
        }

        /// <summary>
        ///     Returns the history for an entity between <paramref name="from" /> to <paramref name="to" />
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="minimalResponse"></param>
        /// <returns></returns>
        public static async Task<HistoryListObject> GetHistory(string entityId, DateTime from, DateTime to, bool minimalResponse)
        {
            return await GetHistory(entityId, from, to, minimalResponse, false);
        }

        /// <summary>
        ///     Returns an array of state changes in the past. Each object contains further details for the entities
        /// </summary>
        public static async Task<HistoryListObject> GetHistory(string entityId, DateTimeOffset startDate, DateTimeOffset endDate, bool minimalResponse,
                                                         bool significatChangesOnly)
        {
            string request = $"api/history/period/{startDate.UtcDateTime:yyyy-MM-dd\\THH:mm:ss}";
            request += $"?filter_entity_id={entityId}";
            request += $"&end_time={endDate.UtcDateTime:yyyy-MM-dd\\THH:mm:ss}";

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
                return (await Get<List<HistoryListObject>>(request)).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.Log($"unable to fetch history for {entityId}");

                return new HistoryListObject();
            }
        }
    }
}