using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class Vehicles
    {
        public int Id { get; set; }

        [Display(Name = "Registration Number")]
        public string RegNumber { get; set; }

        [Display(Name = "Time of Parking")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime TimeOfParking { get; set; }
        public string Color { get; set; }

        [Display(Name = "Number of Wheels")]
        [Range(0,10, ErrorMessage = "No more that 10 Wheels")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Vehicle Brand")]
        public string Brand { get; set; }

        [Display(Name = "Vehicle Model")]
        public string Model { get; set; }


        //Forigen Keys
        public int VehicleTypesId { get; set; }
        public int MembersId { get; set; }

        //Navigation Properties

        public VehicleTypes VehicleTypes { get; set; }
        public Members Members { get; set; }

        

    }
}
