﻿using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using AGL.CodeChallenge.Services;
using AGL.CodeChallenge.Common.Models;

namespace AGL.CodeChallenge.ConsoleApp
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CatsAlphabeticalOrder" /> class.
    /// </summary>
    /// <param name="peopleService">IPeopleService</param>
    /// <param name="logger">ILogger</param>
    public class CatsAlphabeticalOrder 
    {
        private IPeopleService peopleService;
        private readonly ILogger<CatsAlphabeticalOrder> logger;

        private const string UnexpectedErrorString = "An expected error occured while the AGL API is being called";
        private const string NoResultFoundString = "There is no data";
        private const string ResultFoundString = "Data found";

        public CatsAlphabeticalOrder(IPeopleService peopleService, ILogger<CatsAlphabeticalOrder> logger)
        {
            this.peopleService = peopleService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets Pets owner and cat details
        /// </summary>
        /// <param></param>
        /// <returns>
        /// Pets genders and cat names
        /// </returns>
        public async Task GetPersonAndPetsAsync()
        {
            try
            {
                var personList = await this.peopleService.GetPersonPetsAsync();

                if(personList == null || personList.Count == 0)
                {
                    this.logger.LogInformation(NoResultFoundString);
                    Console.WriteLine(NoResultFoundString);
                    return;
                }

                this.logger.LogInformation(ResultFoundString);

                // Group cats by owner's gender and sort ASC
                var personListGroupedPerGenderCatNames = personList
                .Where(p => p.Pets != null && p.Pets.Any())
                .GroupBy(g => g.Gender)
                .Select(
                    group => new PersonPetViewModel
                    {
                        OwnerGender = group.Key,
                        CatNames = group
                            .SelectMany(p => p.Pets)
                            .Where(p => p.Type.Equals("Cat", StringComparison.OrdinalIgnoreCase))
                            .Select(pet => pet.Name)
                            .OrderBy(name => name)
                    });

                foreach (var ownerGender in personListGroupedPerGenderCatNames)
                {
                    Console.WriteLine(ownerGender.OwnerGender);
                    foreach (var catNames in ownerGender.CatNames)
                    {
                        Console.WriteLine($" * {catNames}");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, UnexpectedErrorString);
                Console.WriteLine(UnexpectedErrorString);
            }

            Console.ReadLine();
        }
    }
}
