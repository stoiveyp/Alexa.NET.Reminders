using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class RelativeTrigger : Trigger
    {
        public const string TriggerType = "SCHEDULED_RELATIVE";
        public override string Type => TriggerType;

        public RelativeTrigger()
        {
        }

        public RelativeTrigger(int offsetInSeconds)
        {
            OffsetInSeconds = offsetInSeconds;
        }

        [JsonProperty("offsetInSeconds")]
        public int OffsetInSeconds { get; set; }
    }
}