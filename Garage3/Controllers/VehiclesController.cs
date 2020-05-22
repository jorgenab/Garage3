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
        public async Task<IActionResult> Index()
        {
            var model = await mapper.ProjectTo<DetailsViewModel>(_context.Vehicles).ToListAsync();
            return View(model);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNumber,TimeOfParking,Color,NumberOfWheels,Brand,Model,VehicleTypesId,MembersId")] Vehicles vehicles)
        {
            if (id != vehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicles);
                    await _context.SaveChangesAsync();
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
    }
}
