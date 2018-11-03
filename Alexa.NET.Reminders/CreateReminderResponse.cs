using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response
{
    public class CreateReminderResponse
    {
        [JsonProperty("alertToken")]
        public string AlertToken { get; set; }

        [JsonProperty("createdTime")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("updatedTime")]
        public DateTime UpdatedTime { get; set; }

        [JsonProperty("status"),JsonConverter(typeof(StringEnumConverter))]
        public ReminderStatus Status { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}