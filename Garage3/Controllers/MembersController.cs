﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;

namespace Garage3.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public MembersController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Filter(string PersonalNumber, int? vehicleType)
        {

            var model = string.IsNullOrWhiteSpace(PersonalNumber) ?
                   _context.Members :
                    _context.Members.Where(rn => rn.PersonNr
                                 .Contains(PersonalNumber));
            


            return View(nameof(Index), await model.ToListAsync());
        }

        // GET: Members1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: Members1/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var members = await _context.Members
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (members == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(members);
        //}
        public async Task<IActionResult> Details(int? id)
        {
            var vehicle = await mapper.ProjectTo<MemberDetailsViewModel>(_context.Members)
                .FirstOrDefaultAsync(s => s.Id == id);

            return View(vehicle);
        }



        // GET: Members1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonNr,FristName,LastName")] Members members)
        {
            
            if (ModelState.IsValid)
            {
                var findPersonNumber = _context.Members
                    .Where(rn => rn.PersonNr == members.PersonNr).ToList();
                if (findPersonNumber.Count == 0)
                {
                    _context.Add(members);
                }
                else
                {
                    ModelState.AddModelError("PersonNr", "Member with same Person Number already Exist");
                    return View();

                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return View(members);
        }

        // GET: Members1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        // POST: Members1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonNr,FristName,LastName")] Members members)
        {
            if (id != members.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.Id))
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
            return View(members);
        }

        // GET: Members1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // POST: Members1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var members = await _context.Members.FindAsync(id);
            _context.Members.Remove(members);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
