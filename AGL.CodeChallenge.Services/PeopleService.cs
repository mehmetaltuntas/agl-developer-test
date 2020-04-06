using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using AGL.CodeChallenge.Common.Configuration;
using AGL.CodeChallenge.Common.Models;

namespace AGL.CodeChallenge.Services
{
    public class PeopleService : IPeopleService
    {
        private static readonly HttpClient httpClient=new HttpClient();
        private AppSettings appSettings;

        public PeopleService(AppSettings appSettings)
        {
            this.appSettings = appSettings;
            httpClient.BaseAddress = new Uri(this.appSettings.AGLDeveloperTest.Url);
        }

        public async Task<List<Person>> GetPersonPetsAsync()
        {
            List<Person> person = null;

            var httpResponse = await httpClient.GetAsync(this.appSettings.AGLDeveloperTest.Endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                person = JsonConvert.DeserializeObject<List<Person>>(content);
            }

            return person;
        }
    }
}
