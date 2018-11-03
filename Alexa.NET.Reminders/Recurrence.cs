using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class Recurrence
    {
        public Recurrence() { }

        public Recurrence(string frequency, string[] days)
        {
            Frequency = frequency;
            Days = days;
        }

        [JsonProperty("freq", NullValueHandling = NullValueHandling.Ignore)]
        public string Frequency { get; set; }

        [JsonProperty("byDay",NullValueHandling = NullValueHandling.Ignore)]
        public string[] Days { get; set; }
    }
}