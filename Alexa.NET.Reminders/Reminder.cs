using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class Reminder
    {
        [JsonProperty("requestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("trigger")]
        public Trigger Trigger { get; set; }

        [JsonProperty("alertInfo")]
        public AlertInformation AlertInformation { get; set; }

        [JsonProperty("pushNotification")]
        public PushNotification PushNotification { get; set; }
        
    }
}
