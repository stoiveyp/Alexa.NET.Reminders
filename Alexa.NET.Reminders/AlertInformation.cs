using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class AlertInformation
    {
        public AlertInformation() { }

        public AlertInformation(IEnumerable<SpokenContent> content)
        {
            Spoken = new SpokenInformation(content);
        }

        [JsonProperty("spokenInfo")]
        public SpokenInformation Spoken { get; set; }
    }
}