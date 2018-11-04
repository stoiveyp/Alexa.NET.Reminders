using System;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    [JsonConverter(typeof(TriggerConverter))]
    public abstract class Trigger
    {
        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("scheduledTime",NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ScheduledTime { get; set; }

        [JsonProperty("timeZoneId", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZoneId { get; set; }
    }
}