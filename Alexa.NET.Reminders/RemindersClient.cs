using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Reminders;
using Alexa.NET.Request;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class RemindersClient
    {
        public const string ReminderDomain = "https://api.amazonalexa.com";
        public HttpClient Client { get; set; }

        public RemindersClient(SkillRequest request) : this(
            request.Context.System.ApiEndpoint,
            request.Context.System.ApiAccessToken)
        { }

        public RemindersClient(string endpointUrl, string accessToken)
        {
            var client = new HttpClient { BaseAddress = new Uri(endpointUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client = client;
        }

        public RemindersClient(HttpClient client)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(ReminderDomain, UriKind.Absolute);
            }
            Client = client;
        }

        public async Task<CreateReminderResponse> Create(Reminder reminder)
        {
            var message = await Client.PostAsync(
                new Uri("/v1/alerts/reminders", UriKind.Relative),
                new StringContent(JsonConvert.SerializeObject(reminder)));

            if (message.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}");
            }

            return JsonConvert.DeserializeObject<CreateReminderResponse>(await message.Content.ReadAsStringAsync());
        }
    }
}
