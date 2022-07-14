using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CIS.Service.Client.Extensions
{
    public static class HttpExtension
    {
        public static void ConfigureRequestMessage(this HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultSettings.ContentType));
            requestMessage.Headers.AcceptCharset.ParseAdd(DefaultSettings.Charset);
        }

        public static void ConfigureHttpClient(this HttpClient client)
        {
            client.Timeout = new TimeSpan(0, 0, 5, 0);
        }
    }
}
