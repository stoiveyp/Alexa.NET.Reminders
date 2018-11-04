using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class SpokenInformation
    {
        public SpokenInformation() { }

        public SpokenInformation(IEnumerable<SpokenContent> content)
        {
            Content = content.ToList();
        }

        [JsonProperty("content")]
        public IList<SpokenContent> Content { get; set; }
    }
}