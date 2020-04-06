using System;
using System.Collections.Generic;

namespace AGL.CodeChallenge.Common.Models
{
    public class PersonPetViewModel
    {
        public string OwnerGender { get; set; }

        public IEnumerable<string> CatNames { get; set; }
    }
}
