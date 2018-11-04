using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class AbsoluteTriggerInformation : TriggerInformation
    {
        public override string Type => AbsoluteTrigger.TriggerType;

        [JsonProperty("recurrence", NullValueHandling = NullValueHandling.Ignore)]
        public Recurrence Recurrence { get; set; }
    }
}