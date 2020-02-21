using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Extensions.Http;

using WebApi.Helpers;
using WebApi.Interfaces;

using Services.Common.Extensions;

namespace WebApi.Extensions
{
    /// <summary>
    /// Helper class for setting up access to the au
    /// </summary>
    public static class AcquiringBankServiceExtension
    {
        /// <summary>
        /// Creates and instance of the HTTPClient for accessing acquiring bank.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        { 
            ConfigExtension config = new ConfigExtension();
            configuration.GetSection("ConfigExtension").Bind(config);
            config.ValidateAndThrow();
            
            services.AddHttpClient<IAcquiringBank, AcquiringBank>(client =>
                {
                    client.BaseAddress = new Uri(config.AcquiringBankUri);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddPolicyHandler(GetRetryPolicy());

                // Todo
                // Circuit breaker policy
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (exception, timespan, retryAttempt, context) =>
                {
                    // Todo
                    // Add logging here
                });
        }
    }
}
