#region

using Newtonsoft.Json;
using UnityEngine;

#endregion

    public class UnitSystemObject
    {
        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("mass")]
        public string Mass { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("temperature")]
        public string Temperature { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }
    }