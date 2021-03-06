﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage3.Models;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context (DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }

        public DbSet<Garage3.Models.Vehicles> Vehicles { get; set; }

        public DbSet<Garage3.Models.Members> Members { get; set; }
        public DbSet<Garage3.Models.VehicleTypes> VehicleTypes { get; set; }
    }
}
