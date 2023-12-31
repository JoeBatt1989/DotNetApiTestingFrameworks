﻿using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace IntegrationTests.Utils
{
    public static class HttpRequestMessageExtensions
    {
        internal static HttpRequestMessage Clone(this HttpRequestMessage input)
        {
            var request = new HttpRequestMessage(input.Method, input.RequestUri)
            {
                Content = input.Content
            };

            foreach (var prop in input.Properties)
            {
                request.Properties.Add(prop);
            }

            foreach (var header in input.Headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return request;
        }

        public static void AddJsonBody<T>(this HttpRequestMessage input, T body)
        {
            if (body == null || (body is string b && string.IsNullOrEmpty(b)))
            {
                input.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                return;
            }

            input.Content = new ObjectContent<T>(body, new JsonMediaTypeFormatter());
        }
    }
}
