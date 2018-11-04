using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public abstract class ReminderCore
    {
        [JsonProperty("trigger")]
        public Trigger Trigger { get; set; }

        [JsonProperty("alertInfo")]
        public AlertInformation AlertInformation { get; set; }

        [JsonProperty("pushNotification")]
        public PushNotification PushNotification { get; set; }
    }
}