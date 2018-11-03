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
                Assert.Equal("/v1/alerts/reminders", req.RequestUri.PathAndQuery);
                var content = JsonConvert.DeserializeObject<Reminder>(await req.Content.ReadAsStringAsync());
                Assert.IsType<AbsoluteTrigger>(content.Trigger);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseBody) };
            }));
            var client = new RemindersClient(netClient);
            var response = await client.Create(reminder);
            Assert.NotNull(response);
            Assert.IsType<CreateReminderResponse>(response);
            Assert.Equal("abcdef",response.AlertToken);
        }
    }
}
