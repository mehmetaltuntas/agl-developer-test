using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using AGL.CodeChallenge.Common.Configuration;
using AGL.CodeChallenge.Common.Models;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace AGL.CodeChallenge.Services
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PeopleService" /> class.
    /// </summary>
    /// <param name="httpClient">HttpClient</param>
    /// <param name="appSettings">AppSettings</param>
    public class PeopleService : HttpServiceBase, IPeopleService
    {
        private readonly ILogger<PeopleService> logger;
        private AppSettings appSettings;

        public PeopleService(HttpClient httpClient, ILogger<PeopleService> logger, AppSettings appSettings)
            : base(httpClient, logger)
        {
            this.appSettings = appSettings;
            this.logger = logger;
            httpClient.BaseAddress = new Uri(this.appSettings.AGLDeveloperTest.Url);
        }

        /// <summary>
        /// Establish a connection through the API
        /// </summary>
        /// <param></param>
        /// <returns>
        /// A list of person details including pets
        /// </returns>
        public async Task<List<Person>> GetPersonPetsAsync()
        {
            List<Person> person = null;

            var httpResponse = await GetAsync(this.appSettings.AGLDeveloperTest.Endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                this.logger.LogInformation("AGL API call occurred");

                var content = await httpResponse.Content.ReadAsStringAsync();
                person = JsonConvert.DeserializeObject<List<Person>>(content);
            }

            return person;
        }
    }
}
