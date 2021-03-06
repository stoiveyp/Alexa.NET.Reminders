﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<ReminderChangedResponse> Create(Reminder reminder)
        {
            var message = await Client.PostAsync(
                new Uri("/v1/alerts/reminders", UriKind.Relative),
                new StringContent(JsonConvert.SerializeObject(reminder),Encoding.UTF8,"application/json"));

            if (message.StatusCode != HttpStatusCode.Created)
            {
                var body = await message.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}, body: {body}");
            }

            return JsonConvert.DeserializeObject<ReminderChangedResponse>(await message.Content.ReadAsStringAsync());
        }

        public async Task<ReminderChangedResponse> Update(string alertToken, Reminder reminder)
        {
            var message = await Client.PutAsync(
                new Uri("/v1/alerts/reminders/"+System.Net.WebUtility.UrlEncode(alertToken), UriKind.Relative),
                new StringContent(JsonConvert.SerializeObject(reminder),Encoding.UTF8,"application/json"));

            if (message.StatusCode != HttpStatusCode.OK)
            {
                var body = await message.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}, body:{body}");
            }

            return JsonConvert.DeserializeObject<ReminderChangedResponse>(await message.Content.ReadAsStringAsync());
        }

        public async Task<GetReminderResponse> Get()
        {
            var message = await Client.GetAsync(new Uri("/v1/alerts/reminders", UriKind.Relative));

            if (message.StatusCode != HttpStatusCode.OK)
            {
                var body = await message.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}, body:{body}");
            }

            return JsonConvert.DeserializeObject<GetReminderResponse>(await message.Content.ReadAsStringAsync());
        }

        public async Task<ReminderInformation> Get(string alertToken)
        {
            var message = await Client.GetAsync(
                new Uri("/v1/alerts/reminders/" + System.Net.WebUtility.UrlEncode(alertToken), UriKind.Relative));

            if (message.StatusCode != HttpStatusCode.OK)
            {
                var body = await message.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}, body:{body}");
            }

            return JsonConvert.DeserializeObject<ReminderInformation>(await message.Content.ReadAsStringAsync());
        }

        public async Task Delete(string alertToken)
        {
            var message = await Client.DeleteAsync(
                new Uri("/v1/alerts/reminders/" + System.Net.WebUtility.UrlEncode(alertToken), UriKind.Relative));

            if (message.StatusCode != HttpStatusCode.OK)
            {
                var body = await message.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Unexpected result: Status {message.StatusCode}, body:{body}");
            }
        }
    }

    public class GetReminderResponse
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("alerts")]
        public ReminderInformation[] Alerts { get; set; }

        [JsonProperty("links")]
        public Dictionary<string,string> Links { get; set; }
    }
}
