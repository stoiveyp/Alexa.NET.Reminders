using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response
{
    public class ReminderChangedResponse
    {
        [JsonProperty("alertToken")]
        public string AlertToken { get; set; }

        [JsonProperty("createdTime")]
        public string CreatedTime { get; set; }

        [JsonProperty("updatedTime")]
        public string UpdatedTime { get; set; }

        [JsonProperty("status"),JsonConverter(typeof(StringEnumConverter))]
        public ReminderStatus Status { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}