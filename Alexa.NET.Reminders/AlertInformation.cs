using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}