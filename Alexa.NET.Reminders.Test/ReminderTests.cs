using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Reminders.Test
{
    public class ReminderTests
    {
        [Fact]
        public async Task ReminderSerializesProperly()
        {
            var reminder = new Reminder
            {
                RequestTime = DateTime.Parse("2016-09-22T19:04:00.672"),
                Trigger = new AbsoluteTrigger(
                    DateTime.Parse("2018-09-22T19:00:00.000"),
                    "America/Los_Angeles",
                    new Recurrence("WEEKLY", new[] { "MO" })),
                AlertInformation = new AlertInformation(new []{new SpokenContent("walk the dog", "en-US")}),
                PushNotification = PushNotification.Enabled
            };

            Assert.True(Utility.CompareJson(reminder, "reminder.json"));
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
