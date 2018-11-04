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

                return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(responseBody) };
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
            var reminder = Utility.ExampleFileContent("reminderInformation.json");
            var netClient = new HttpClient(new ActionMessageHandler(async req =>
            {
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal("/v1/alerts/reminders/abcdef", req.RequestUri.PathAndQuery);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(reminder) };
            }));

            var client = new RemindersClient(netClient);
            var response = await client.Get("abcdef");

            Assert.NotNull(response);
            Assert.IsType<ReminderInformation>(response);
            Assert.Equal("79559745-635b-44cb-ba9d-9f364d41600d", response.AlertToken);
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

        //    var client = new RemindersClient("https://api.eu.amazonalexa.com", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjEifQ.eyJhdWQiOiJodHRwczovL2FwaS5hbWF6b25hbGV4YS5jb20iLCJpc3MiOiJBbGV4YVNraWxsS2l0Iiwic3ViIjoiYW16bjEuYXNrLnNraWxsLjhlNjk3YzcyLTgwNTQtNDQ1ZS04YTA0LTMwNzU4Nzc4M2VjYyIsImV4cCI6MTU0MTMyMDc5NywiaWF0IjoxNTQxMzE3MTk3LCJuYmYiOjE1NDEzMTcxOTcsInByaXZhdGVDbGFpbXMiOnsiY29uc2VudFRva2VuIjoiQXR6YXxJd0VCSU5xTmNCRVg1cGhSa195TmFBY2VndlhMMzlOVnFWcWE2SGZfNHc5di10b3Z4Y2VjTjFSbGpyZEs2bVE4QTREaE82RmYyT1NkdDlPS3lOclc4cTRNb3c3YmE5cGtTV3JZQl8wQ3hSVktWSjdTOFd1MHdibm1CVmNxWldIWHlRcERnYVRKMktxNUNJczd5bks2QVVLd29hR2p1Wm5MMHdDNEp1OVI2OUwySXNuUGVHb2R1dV9MaXM3Zk0xcGo0Mm5Ia2NnSmNKZTlQRXplbC1lckwwNm9mU3ZLUGxSWGsyUjlONEVJcjREMlF1UVpnNl85ZkF2dkhCWlBMYmo1NHBlRlRrWG5aWG9fX0FoZWZHelhDaUZCTWZOYTlOOWFPSTBCMmg3aFdTQ3MyU25TZXhCUU00ZGxvUDV2Z0hBbmhxQ0x0cTFkV0lnckQ4UkNvT1ZMOFZ0V2s0VW81a1JTeFN4WkppcFQzM2dsV1hOdDFHY284WjZFT0tPMHVFU2lFU0I1Y0xMakZJNEthUTI4dDB3VlJwdmNnSWRvMWFkcjRhYThoWEFLb0U5TXE4MXVVbXpkdUJzbHd5RkItRDZONjFFRHhtcTNYV3lINXBxXzRkZnRzeDExZW1YX0RtUHIxS2t1c0V2d3NiYnJWNG1HSXdidEtaVi1pbG1EemR3MmIyRDhFd2J1SXJaUWNRaGYwa2UxTWh6dlNHTTdSY0tweWdNV1FzMU5fRFZUUFl6NVFDTV84eXV6U3dTbkhWWjk2TThGN1ZwcW1VMmJxeDVISzZ1dDJYcXQyd0F4QW5Bb2ZMb21RMU9XX28zNFZEUFFGVnFkWnJWVlVqVEFfeUpGRVRxR0YtUSIsImRldmljZUlkIjoiYW16bjEuYXNrLmRldmljZS5BRk5ITjZGV01MWEdQQlRNSkpQU1dGQ1VZTU5GRERCQlRDRlZBRlpIN09WV1gzRjRHWlUyS0dYUjdEWFBWR0ZZNlVBSk5HNEdHNURWQ0FGTUpKNFdEQklIVkJKVktHV0tFVFZCTDRHQlYyUFhCVkpaUTJUWEpLTU9MV1RZVFo3SU9TTzVNWUNJR1ZMM1ZKWVI2MlhJVVZURUlLUUpNTEYySERJWk5KVkVHQ1pJRlVBWUsyN0ZNIiwidXNlcklkIjoiYW16bjEuYXNrLmFjY291bnQuQUZOUlhaSEFWWDdDWUJFT0dVUDc1UFNIWE9LWVZaT1RZNEVMRTJYTlNXQktaV0xHTVFJUDJBVE42UEVUWlczTkNLWVJBNFNDTllCWkhER1NKTFNOSkVTRTRCUTNPWTM1TUlKVVcyT05BNEFHUlEzSTJIUEpIU0pCN1NJMjRGSkdaTFVWQlFBTE1JNDI0NE1PRFlFM0FKSFlJTUhNU0ZWMlE3U1c2RzRKTzM2S0Q0N1pNMkozMkczSExON0I2U1ZNNjNIUFFRR1FUT0U3WE9ZIn19.DKel85wH88-Ad4Twrq5CDpjpL1dkSU8nhZ2OyRU--sahYHIxkrfRNUA0kcngVu7hnNK-zpWIOSuoEVWDTSqH0E9DY6Iw7Fq-4748NKHdsHp56oID6RFE4ptmtGw3Eh047RliohkuUeC50mMrMBRAmIshEZCeGTyHxxGCsC1U-Fn270twsY8CBtyP3GelAx9yAYVm8nSxJU6Wg-RO3SGiPOVGzuBmkzisMns8-iGQ8SyjqWTJKJoAOMTjYxBfLlLXtz7IFUL5Uh9x7TzNgHQYtwjuIT4jG6UY6GLPs3y2Gurtj6ClBHPBC_Nrx_Ac-Ywshw4BytsOx0zRBXUFl4iYPQ");
        //    var response = await client.Create(reminder);
        //}
    }
}
