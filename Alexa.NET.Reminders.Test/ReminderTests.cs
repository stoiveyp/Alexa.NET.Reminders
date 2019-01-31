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

            var client = new RemindersClient(RemindersClient.ReminderDomain,
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjEifQ.eyJhdWQiOiJodHRwczovL2FwaS5hbWF6b25hbGV4YS5jb20iLCJpc3MiOiJBbGV4YVNraWxsS2l0Iiwic3ViIjoiYW16bjEuYXNrLnNraWxsLmU3ZWZmZGViLWYzOWEtNGNkZi1hOWFlLTIxMmExMTc5ODQ1ZCIsImV4cCI6MTU0ODYwMDc1MiwiaWF0IjoxNTQ4NjAwNDUyLCJuYmYiOjE1NDg2MDA0NTIsInByaXZhdGVDbGFpbXMiOnsiaXNEZXByZWNhdGVkIjoidHJ1ZSIsImNvbnNlbnRUb2tlbiI6IkF0emF8SXdFQklQMUR4aTZhdkxyRTR6di1tSkdTeWx4cnFtMjJWT3ZPYnJ0ZFJCQW1UcENvb1h4QUswTjduNGJFRkthcG81QmhTaHg2cmc3ZzFLcGh5OVZVeC1ocGdwMVc4aGZ3Z0d0WFFUWVlSNWhLNW1rSGJTeGhISG41NklLekpXcmhNalRqTDN1RHowUkdTNW92MjFZWDQ1Tjkzcll2eWlVN1JFX0gzY2xxakpKRE43Y3phTGxMWTk3M054QmxHYWpVdUlhQXlpLXN0WVFKcFVBOW5CRDR1OW15VFJ0V2djeDhQQmJHT1JYVHoxbGl4WjEtcGZYUUFsd0ZidEUyUVRkYlBpLUZhdE9UNWZ5d2JiUFFfenltbmJDLXd2SlVtdWROOWdfbUZud0V6N2laTlVSelBqOTBwMUdKVzVTelU3MFMwaFhvNmNzbVJOMWtrV3NVT25zaXRQNVo1bjRyNTZHRGJpaDc5V2dYWDFlMWtJai04VmhHSWNJa2JqRmJjSHdkX1VjQTlRTnk2aTJnWmpUbFV2YTFBdW5COFpnNl9yYnhVS04tdTZ6QzY2Q1BrT1BqakIzR2dlZkxFdTN4UkxRcnBnZTNQWmc3OVQ1OGtOSGZ5anRHZldUUmktYXlaSEdzcTQ2cFZua2ZCYzBuYkZ3Vy10Y3JoQU16VURVQ0RQcnJxaTlUamo5VjhodTA5ODAtWnVTWE1XODl4cWRkIiwiZGV2aWNlSWQiOiJBMzRNWFNXWFRQNlgzUSIsInVzZXJJZCI6ImFtem4xLmFzay5hY2NvdW50LkFHSVNIUlVGMk5LNDQyVVJKUkMzSjZERk82QUhKV1FPVVNERjZSNUxBUkE0QkhOMlZEV1gyNkI3NEdXRFVXNjRHTlJONUZMVlZZQkFXUk1FS1lCT0k0WUY3MldYTUFGVFVPUFU3M01VSlpQQ01ZRzRJSTVTR0xYU1FGWk9MVFJXRzRSU0lZWlhFN05HS0IyMlpMTFVMQVREQ0hPUkRTR0VNSURPWElJVzVQR1RSUkMyVVlSWjJGVUpDWEozSk9PQTM0NjdaRUxGRFpEUkZLWSJ9fQ.kmOYd9FcO6IIYXJ94r7aWE00Z-DXjNXYgGnmQv1EmicORGrITxbSRjETFMd60r-JoPOPoG4VdJspAiS8OPXdQeYYx0Cb9x2UO6pA3e6PKevdlggrFtKy21-WXiO7sFnRZtkp0DFtG462z4ssLutrMTPpjgnz2nMSYLbVb3oFAQxYCCLiulz2J3V1EKZBCjd6au8KiCXqVpiClPcD0QuG7wAEVTozEypbbMaq1dTTOXa1QinRz0R1yRq1P9e5NQLlav_wUa27dka3iGyXP-BUgKligqGE51VlihWdWZqymgzKPm7J2zRDSMeFrnwv_yedrPhP8KH8Ulx6pQ1o8hxq0Q");
            var response = await client.Create(reminder);
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
