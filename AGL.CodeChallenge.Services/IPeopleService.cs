using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AGL.CodeChallenge.Common.Models;

namespace AGL.CodeChallenge.Services
{
    public interface IPeopleService
    {
        Task<List<Person>> GetPersonPetsAsync();
    }
}
