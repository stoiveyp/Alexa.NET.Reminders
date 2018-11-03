using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Alexa.NET.Request;

namespace Alexa.NET.Response
{
    public class RemindersClient
    {
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
            Client = client;
        }
    }
}
