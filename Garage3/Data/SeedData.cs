using Bogus;
using Garage3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            var option = services.GetRequiredService<DbContextOptions<Garage3Context>>();

            using (var context = new Garage3Context(option))
            {
                //var fake = new Faker("sv");
                var vehicles = new List<VehicleTypes>();
                var typearr = new string[5] { "Airplane", "Boat", "Bus", "Car", "Moterbike" };
                if (!context.VehicleTypes.Any())
                {
                    for (int i = 0; i<5; i++)
                    {
                        string ftype = typearr[i];
                        var vehicleType = new VehicleTypes
                        {
                            TypeOfVehicle = ftype,
                        };
                        vehicles.Add(vehicleType);
                    }
                    context.AddRange(vehicles);
                    context.SaveChanges();
                }
            }

        }
    }
}
