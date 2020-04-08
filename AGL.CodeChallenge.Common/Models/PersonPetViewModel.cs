using System;
using System.Collections.Generic;

namespace AGL.CodeChallenge.Common.Models
{
    public class PersonPetViewModel
    {
        /// <summary>
        /// OwnerGender
        /// </summary>
        public string OwnerGender { get; set; }

        /// <summary>
        /// CatNames
        /// </summary>
        public IEnumerable<string> CatNames { get; set; }
    }
}
