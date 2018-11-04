using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Reminders
{
    public class ReminderInformation:ReminderCore
    {
        [JsonProperty("alertToken")]
        public string AlertToken { get; set; }

        [JsonProperty("createdTime")]
        public string CreatedTime { get; set; }

        [JsonProperty("updatedTime")]
        public string UpdatedTime { get; set; }
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public ReminderStatus Status { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
