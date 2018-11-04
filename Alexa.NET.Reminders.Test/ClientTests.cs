using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Xunit;

namespace Alexa.NET.Reminders.Test
{
    public class ClientTests
    {
        [Fact]
        public void CreationFromSkillRequest()
        {
            var request = new SkillRequest
            {
                Context = new Context
                {
                    System = new AlexaSystem
                    {
                        ApiAccessToken = "token",
                        ApiEndpoint = "https://api.amazonalexa.com"
                    }
                }
            };

            var client = new RemindersClient(request);
            Assert.Equal("token", client.Client.DefaultRequestHeaders.Authorization.Parameter);
            Assert.Equal("api.amazonalexa.com", client.Client.BaseAddress.Host);
        }

        [Fact]
        public void CreationFromClient()
        {
            var netClient = new HttpClient();
            var client = new RemindersClient(netClient);
            Assert.Equal(netClient, client.Client);
        }

        [Fact]
        public async Task CreateGeneratesExpectedCall()
        {
            var responseBody = Utility.ExampleFileContent("createresponse.json");
            var reminder = Utility.ExampleFileContent<Reminder>("reminder.json");
            var netClient = new HttpClient(new ActionMessageHandler(async req =>
            {
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal("/v1/alerts/reminders", req.RequestUri.PathAndQuery);
                var content = JsonConvert.DeserializeObject<Reminder>(await req.Content.ReadAsStringAsync());
                Assert.IsType<AbsoluteTrigger>(content.Trigger);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseBody) };
            }));
            var client = new RemindersClient(netClient);
            var response = await client.Create(reminder);
            Assert.NotNull(response);
            Assert.IsType<ReminderChangedResponse>(response);
            Assert.Equal("abcdef", response.AlertToken);
        }

        [Fact]
        public async Task UpdateReminderGeneratesExpectedCall()
        {
            var responseBody = Utility.ExampleFileContent("createresponse.json");
            var reminder = Utility.ExampleFileContent<Reminder>("reminder.json");
            var netClient = new HttpClient(new ActionMessageHandler(async req =>
            {
                Assert.Equal(HttpMethod.Put,req.Method);
                Assert.Equal("/v1/alerts/reminders/abcdef", req.RequestUri.PathAndQuery);
                var content = JsonConvert.DeserializeObject<Reminder>(await req.Content.ReadAsStringAsync());
                Assert.IsType<AbsoluteTrigger>(content.Trigger);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseBody) };
            }));
            var client = new RemindersClient(netClient);
            var response = await client.Update("abcdef",reminder);
            Assert.NotNull(response);
            Assert.IsType<ReminderChangedResponse>(response);
            Assert.Equal("abcdef", response.AlertToken);
        }

        [Fact]
        public async Task GetReminderGeneratesExpectedCall()
        {
            Assert.True(false);
        }

        [Fact]
        public async Task GetAllRemindersGeneratesExpectedCall()
        {
            Assert.True(false);
        }


        [Fact]
        public async Task DeleteReminderGeneratesExpectedCall()
        {
            Assert.True(false);
        }

        //[Fact]
        //public async Task FakeTest()
        //{
        //    var reminder = new Reminder
        //    {
        //        RequestTime = DateTime.UtcNow,
        //        Trigger = new RelativeTrigger(12 * 60 * 60),
        //        AlertInformation = new AlertInformation(new[] { new SpokenContent("it's a test", "en-GB") }),
        //        PushNotification = PushNotification.Disabled
        //    };
        //    var total = JsonConvert.SerializeObject(reminder);

        //    var client = new RemindersClient("https://api.eu.amazonalexa.com","");
        //    var response = await client.Create(reminder);
        //}
    }
}
