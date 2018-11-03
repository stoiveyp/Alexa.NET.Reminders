using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class AbsoluteTrigger : Trigger
    {
        public const string TriggerType = "SCHEDULED_ABSOLUTE";
        public override string Type => TriggerType;

        [JsonProperty("scheduledTime")]
        public DateTime ScheduledTime { get; set; }

        [JsonProperty("timeZoneId", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZoneId { get; set; }

        [JsonProperty("recurrence",NullValueHandling = NullValueHandling.Ignore)]
        public Recurrence Recurrence { get; set; }
    }
}