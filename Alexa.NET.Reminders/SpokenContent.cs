using System;
using Newtonsoft.Json;

namespace Alexa.NET.Reminders
{
    public class SpokenContent
    {
        public SpokenContent() { }

        public SpokenContent(string text, string locale)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Locale = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ssml",NullValueHandling = NullValueHandling.Ignore)]
        public string Ssml { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}