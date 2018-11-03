using System;
using System.Net.Http;
using Alexa.NET.Request;
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
            Assert.Equal("token",client.Client.DefaultRequestHeaders.Authorization.Parameter);
            Assert.Equal("api.amazonalexa.com",client.Client.BaseAddress.Host);
        }

        [Fact]
        public void CreationFromClient()
        {
            var netClient = new HttpClient();
            var client = new RemindersClient(netClient);
            Assert.Equal(netClient,client.Client);
        }
    }
}
