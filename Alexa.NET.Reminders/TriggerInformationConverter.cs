using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Reminders
{
    public class TriggerInformationConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);
            var triggerType = jObject.Value<string>("type");

            object target = GetTriggerInformation(triggerType);

            if (target != null)
            {
                serializer.Populate(jObject.CreateReader(), target);
                return target;
            }

            throw new ArgumentOutOfRangeException($"Trigger information type {triggerType} not supported");
        }

        private TriggerInformation GetTriggerInformation(string triggerType)
        {
            if (triggerType == AbsoluteTrigger.TriggerType)
            {
                return new AbsoluteTriggerInformation();
            }

            if (triggerType == RelativeTrigger.TriggerType)
            {
                return new RelativeTriggerInformation();
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsSubclassOf(typeof(TriggerInformation));
        }
    }
}