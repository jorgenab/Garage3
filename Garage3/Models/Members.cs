﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class Members
    {
        public int Id { get; set; }

        [Display(Name = "Personal Number")]
        [Range(10,12)]
        public string PersonNr { get; set; }

        [Display(Name = "First Name")]
        public string FristName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => $"{FristName} {LastName}";
        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
