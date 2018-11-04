using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    [JsonConverter(typeof(TriggerInformationConverter))]
    public abstract class TriggerInformation
    {
        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("timeZoneId", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZoneId { get; set; }

        [JsonProperty("scheduledTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ScheduledTime { get; set; }
    }
}
