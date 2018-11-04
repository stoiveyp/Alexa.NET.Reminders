using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public abstract class ReminderCore
    {
        [JsonProperty("alertInfo")]
        public AlertInformation AlertInformation { get; set; }

        [JsonProperty("pushNotification")]
        public PushNotification PushNotification { get; set; }
    }
}