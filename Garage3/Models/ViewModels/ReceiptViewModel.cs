using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class ReceiptViewModel
    {

        [Display(Name = "Vehicle Parked: ")]
        public string TypeOfVehicle { get; set; }

        [Display(Name = "Member Name: ")]
        public string FullName { get; set; }

        [Display(Name = "Registration No: ")]
        public string RegNumber { get; set; }

        [Display(Name = "CheckIn Time: ")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "CheckOut Time: ")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime CheckOutTime { get; set; }

        [Display(Name = "Total Parking Time: ")]
        public string TotalParkingTime { get; set; }
        [Display(Name = "Total Price: ")]
        public string TotalPrice { get; set; }
    }
}
