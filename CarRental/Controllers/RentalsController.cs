﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using CarRental.Helpers;

namespace CarRental.Controllers
{
    public class RentalsController : Controller
    {
        private readonly CarRentalContext _context;

        public RentalsController(CarRentalContext context)
        {
            _context = context;
        }

        private async Task<bool> ValidateRental(Rental rental)
        {
            bool isValid = true;

            // Check if from date is after To date
            if (rental.From > rental.To)
            {
                ModelState.AddModelError("Rental.From", "Start date cannot be after end date");
                var createRentalViewModel = new CreateRentalViewModel() { Cars = await _context.Cars.ToListAsync(), Rental = rental };
                isValid = false;
            }

            //1. Hämta ut alla bokning som har samma roomId som den nya bokningen
            var rentalsFromDb = await _context.Rentals.Where(r => r.CarId == rental.CarId).ToListAsync();

            //2. Kolla om något av dessa bokningar har överlappande datum
            foreach (var oldRental in rentalsFromDb)
            {
                if (DateHelpers.HasSharedDateIntervals(rental.From, rental.To, oldRental.From, oldRental.To))
                {
                    ModelState.AddModelError("Rental.From", "Date already occupied.");
                    var createRentalViewModel = new CreateRentalViewModel() { Cars = await _context.Cars.ToListAsync(), Rental = rental };
                    isValid = false;
                }
            }

            return isValid;
        }

        // GET: Rentals
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rentals.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var createRentalViewModel = new CreateRentalViewModel() { Cars = await _context.Cars.ToListAsync() };

            return View(createRentalViewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rentee,CarId,Car,From,To")] Rental rental)
        {
            if (!await ValidateRental(rental))
            {
                var createRentalViewModel = new CreateRentalViewModel() { Cars = await _context.Cars.ToListAsync() };

                return View(createRentalViewModel);
            }

            var cars = await _context.Cars.ToListAsync();

            Car car = cars.SingleOrDefault(car => car.Id == rental.CarId);

            if (car == null)
            {
                return NotFound();
            }

            rental.Car = car.Brand + " " + car.Model;

            _context.Add(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Rentals/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rentee,CarId,Car,From,To")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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
            return View(rental);
        }

        // GET: Rentals/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
