using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AGL.CodeChallenge.Services
{
    public abstract class HttpServiceBase
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        protected HttpServiceBase(HttpClient client, ILogger logger)
        {
            this.httpClient = client;
            this.logger = logger;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            try
            {
                var httpResponse = await this.httpClient.GetAsync(uri);
                return httpResponse;
            }
            catch (Exception exception)
            {
                this.logger.LogError("An error has occured during API call. '{RequestUri}' with error message ", uri, exception.InnerException?.Message);
                throw;
            }
        }
    }
}
