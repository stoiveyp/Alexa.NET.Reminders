using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Reminders.Test
{
    public class ReminderTests
    {
        [Fact]
        public void ReminderSerializesProperly()
        {
            var reminder = new Reminder();
            Assert.False(true);
        }

        [Fact]
        public void AbsoluteTriggerSerializeProperly()
        {
            var trigger = new AbsoluteTrigger();
            Assert.True(Utility.CompareJson(trigger,"absoluteTrigger.json"));
        }

        [Fact]
        public void RelativeTriggerSerializeProperly()
        {
            var trigger = new RelativeTrigger(7200);
            Assert.True(Utility.CompareJson(trigger, "relativeTrigger.json"));
        }

        [Fact]
        public void SerializerDeserializesProperly()
        {
            Assert.False(true);
        }
    }
}
