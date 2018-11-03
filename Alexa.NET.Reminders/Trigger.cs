using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    [JsonConverter(typeof(TriggerConverter))]
    public abstract class Trigger
    {
        [JsonProperty("type")]
        public abstract string Type { get; }
    }
}