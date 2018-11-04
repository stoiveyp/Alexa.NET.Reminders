using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class RelativeTriggerInformation : TriggerInformation
    {
        [JsonProperty("offsetInSeconds")]
        public int OffsetInSeconds { get; set; }

        public override string Type => RelativeTrigger.TriggerType;
    }
}