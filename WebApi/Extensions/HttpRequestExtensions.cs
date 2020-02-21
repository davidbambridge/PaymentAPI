using System;

using Microsoft.AspNetCore.Http;

namespace WebApi.Extensions
{
    /// <summary>
    /// Extension methods for dealing with HttpRequests
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Extract the URI from a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Uri GetRequestUri(this HttpRequest request)
        {
            UriBuilder builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host
            };

            if (request.Host.Port.HasValue)
                builder.Port = request.Host.Port.Value;

            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();

            return builder.Uri;
        }
    }
}
