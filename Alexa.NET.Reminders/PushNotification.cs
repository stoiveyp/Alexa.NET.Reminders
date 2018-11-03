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

        public static PushNotification Enabled => new PushNotification("ENABLED");
        public static PushNotification Disabled => new PushNotification("DISABLED");
    }
}