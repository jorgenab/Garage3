using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _context;

        public VehiclesController(Garage3Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage3Context = _context.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);
            return View(await garage3Context.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Set<Members>(), "Id", "Id");
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNumber,TimeOfParking,Color,NumberOfWheels,Brand,Model,VehicleTypeId,MemberId")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypeId);
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
            ViewData["MemberId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypeId);
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNumber,TimeOfParking,Color,NumberOfWheels,Brand,Model,VehicleTypeId,MemberId")] Vehicles vehicles)
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
            ViewData["MemberId"] = new SelectList(_context.Set<Members>(), "Id", "Id", vehicles.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleTypes>(), "Id", "Id", vehicles.VehicleTypeId);
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
    }
}
