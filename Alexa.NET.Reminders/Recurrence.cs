using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class Recurrence
    {
        public Recurrence() { }

        public Recurrence(string frequency, IEnumerable<string> days)
        {
            Frequency = frequency;
            Days = days.ToList();
        }

        [JsonProperty("freq", NullValueHandling = NullValueHandling.Ignore)]
        public string Frequency { get; set; }

        [JsonProperty("byDay",NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> Days { get; set; }
    }
}