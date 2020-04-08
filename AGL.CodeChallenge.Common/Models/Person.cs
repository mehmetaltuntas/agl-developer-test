using System;
using System.Collections.Generic;

namespace AGL.CodeChallenge.Common.Models
{
    public class Person
    {
        /// <summary>
        /// Person Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Person Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Person Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Person's Pets
        /// </summary>
        public List<Pet> Pets { get; set; }
    }
}
