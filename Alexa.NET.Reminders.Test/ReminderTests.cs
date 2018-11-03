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
            var trigger = new AbsoluteTrigger
            {
                ScheduledTime = DateTime.Parse("2018-09-22T19:00:00.000"),
                TimeZoneId = "America/Los_Angeles",
                Recurrence = new Recurrence("WEEKLY", new[] {"MO"})
            };
            Assert.True(Utility.CompareJson(trigger,"absoluteTrigger.json"));
            var triggerTest = Utility.ExampleFileContent<Trigger>("absoluteTrigger.json");
            Assert.IsType<AbsoluteTrigger>(triggerTest);
        }

        [Fact]
        public void RelativeTriggerSerializeProperly()
        {
            var trigger = new RelativeTrigger(7200);
            Assert.True(Utility.CompareJson(trigger, "relativeTrigger.json"));
            var triggerTest = Utility.ExampleFileContent<Trigger>("relativeTrigger.json");
            Assert.IsType<RelativeTrigger>(triggerTest);
        }
    }
}
