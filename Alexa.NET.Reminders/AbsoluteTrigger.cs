namespace Alexa.NET.Reminders
{
    public class AbsoluteTrigger : Trigger
    {
        public const string TriggerType = "SCHEDULED_ABSOLUTE";
        public override string Type => TriggerType;
    }
}