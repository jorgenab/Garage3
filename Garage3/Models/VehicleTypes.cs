using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class VehicleTypes
    {
        public int Id { get; set; }
        [Display(Name = "Type of Vehicle")]
        public string TypeOfVehicle { get; set; }
        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
