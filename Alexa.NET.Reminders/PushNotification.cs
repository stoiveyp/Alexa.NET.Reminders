using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class PushNotification
    {
        public PushNotification() { }

        public PushNotification(string status)
        {
            Status = status;
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        public PushNotification Enabled => new PushNotification("ENABLED");
        public PushNotification Disabled => new PushNotification("DISABLED");
    }
}