using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Personal Number")]
        public string PersonNr { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FristName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Number Of Vehicles")]
        public int NumberOfVehicles { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
