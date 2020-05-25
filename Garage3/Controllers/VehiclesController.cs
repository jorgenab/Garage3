using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;
using AutoMapper;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public VehiclesController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var model = mapper.ProjectTo<DetailsViewModel>(_context.Vehicles);
                          
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.RegNumber);
                    break;
                case "Date":
                    model = model.OrderBy(s => s.TimeOfParking);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(s => s.TimeOfParking);
                    break;
                default:
                    model = model.OrderBy(s => s.RegNumber);
                    break;
            }
            //model = await mapper.ProjectTo<DetailsViewModel>(_context.Vehicles).ToListAsync();
            return View(await model.ToListAsync());

        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(p => p.Members)
                .Include(p => p.VehicleTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

           
            return View(vehicles);
        }

        // GET: Vehicles/Create
        public IActionResult Park()
        {
            var memberList = _context.Set<Members>()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),

                    // look for already registered personal No  
                    Text = x.FullName
                }).ToList();

            ViewData["MembersId"] = memberList;

            ViewData["VehicleTypesId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "TypeOfVehicle");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,RegNumber,TimeOfParking,Color,NumberOfWheels,Brand,Model,VehicleTypesId,MembersId")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                vehicles.TimeOfParking = DateTime.Now;
                _context.Add(vehicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembersId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MembersId);
            ViewData["VehicleTypesId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypesId);
            return View(vehicles);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles == null)
            {
                return NotFound();
            }
            ViewData["MembersId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MembersId);
            ViewData["VehicleTypesId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypesId);
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNumber,Color,NumberOfWheels,Brand,Model,VehicleTypesId,MembersId")] Vehicles vehicles)
        {
            if (id != vehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editPostData = await _context.Vehicles.FindAsync(id);
                    if (editPostData == null)
                    {
                        return NotFound();

                    }
                    else
                    {
                        
                        editPostData.RegNumber = vehicles.RegNumber;
                        editPostData.Color = vehicles.Color;
                        editPostData.Brand = vehicles.Brand;
                        editPostData.Model = vehicles.Model;
                        editPostData.NumberOfWheels = vehicles.NumberOfWheels;

                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesExists(vehicles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembersId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MembersId);
            ViewData["VehicleTypesId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypesId);
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> UnPark(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Members)
                .Include(v => v.VehicleTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/UnPark/5
        [HttpPost, ActionName("UnPark")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnParkConfirmed(int id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
        public async Task<IActionResult> DetailView()
        {
            var model = await _context.Vehicles
                .Include(t => t.VehicleTypes)
                .Include(t => t.Members)
                .Select(t => new DetailsViewModel
                {
                    RegNumber = t.RegNumber,
                    TimeOfParking = t.TimeOfParking,
                    Color = t.Color,
                    Brand = t.Brand,
                    NumberOfWheels = t.NumberOfWheels,
                    Model = t.Model,
                    FullName = t.Members.FristName + " " +  t.Members.LastName
                }).ToListAsync();
            return View(model);

        }

        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.Id == id);


            if (vehicle == null)
            {
                return NotFound();
            }

            ReceiptViewModel model = new ReceiptViewModel();
            model.RegNumber = vehicle.RegNumber;

            var MemberId = vehicle.MembersId;
            var VehicleTypesId = vehicle.VehicleTypesId;
            var memberDetails = await _context.Members.FirstOrDefaultAsync(m => m.Id == MemberId);
            var vehicleTypeDetails = await _context.VehicleTypes.FirstOrDefaultAsync(m => m.Id == VehicleTypesId);

            model.FullName = memberDetails.FullName;
            model.TypeOfVehicle = vehicleTypeDetails.TypeOfVehicle;
            model.CheckInTime = vehicle.TimeOfParking;
            model.CheckOutTime = DateTime.Now;
            var totaltime = model.CheckOutTime - model.CheckInTime;
            var lessThanHr = (totaltime.Seconds > 0) ? 1 : 0;


            if (totaltime.Days == 0)
            {
                model.TotalParkingTime = totaltime.Hours + " Hrs " + totaltime.Minutes + " Mins " + totaltime.Seconds + " Secs";
                model.TotalPrice = ((totaltime.Hours + lessThanHr) * 15) + " Kr";
            }
            else
            {
                model.TotalParkingTime = totaltime.Days + "Days" + " " + totaltime.Hours + " hrs " + " " + totaltime.Minutes + " Mins " + +totaltime.Seconds + " Secs";
                model.TotalPrice = (totaltime.Days * 100) + ((totaltime.Hours + lessThanHr) * 15) + "Kr";
            }

            //_context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return View(model);
        }

        // GET: Vehicles/Search By Reg Nr or Vehicle Type
        public async Task<IActionResult> Search(string vehicleType, string regNumber)
        {
            var vehicles = _context.Vehicles;
            var model = await mapper.ProjectTo<DetailsViewModel>(vehicles).ToListAsync();

            model = string.IsNullOrWhiteSpace(vehicleType) ?
                model :
                model.Where(p => p.TypeOfVehicle.ToLower().Equals(vehicleType.ToLower())).ToList();

            model = regNumber == null ?
                model :
                model.Where(m => m.RegNumber.ToLower().Contains(regNumber.ToLower())).ToList();

            return View(nameof(Index), model);
        }

    }
}
