using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartCarRental.Data;
using SmartCarRental.Models;
using SmartCarRental.ViewModels.Cars;

namespace SmartCarRental.Controllers
{
    public class CarRentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CarRentsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CarRents
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            var applicationDbContext = _context.CarRents
                .Include(c => c.Car)
                .Include(c => c.Renter)
                .Where(c => c.RenterId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());
        }
        //Get: RentedCarsIndex
        public async Task<IActionResult> RentedCarsIndex()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var applicationDbContext = _context.CarRents
                .Include(c => c.Car)
                .Include(c => c.Renter)
                .Where(c => c.RenterId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRent = await _context.CarRents
                .Include(c => c.Car)
                .Include(c => c.Renter)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carRent == null)
            {
                return NotFound();
            }

            return View(carRent);
        }

        // GET: CarRents/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Name");
            ViewData["RenterId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: CarRents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RenterId,CarId")] CarRent carRent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carRent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Name", carRent.CarId);
            ViewData["RenterId"] = new SelectList(_context.Users, "Id", "UserName", carRent.RenterId);
            return View(carRent);
        }

        // GET: CarRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRent = await _context.CarRents.Where(c => c.CarId == id).FirstOrDefaultAsync();
            if (carRent == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Name", carRent.CarId);
            ViewData["RenterId"] = new SelectList(_context.Users, "Id", "UserName", carRent.RenterId);
            return View(carRent);
        }

        // POST: CarRents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RenterId,CarId")] CarRent carRent)
        {
            if (id != carRent.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carRent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentExists(carRent.CarId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Name", carRent.CarId);
            ViewData["RenterId"] = new SelectList(_context.Users, "Id", "UserName", carRent.RenterId);
            return View(carRent);
        }

        // GET: CarRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRent = await _context.CarRents
                .Include(c => c.Car)
                .Include(c => c.Renter)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carRent == null)
            {
                return NotFound();
            }

            return View(carRent);
        }

        // POST: CarRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carRent = await _context.CarRents.FindAsync(id);
            _context.CarRents.Remove(carRent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarRentExists(int id)
        {
            return _context.CarRents.Any(e => e.CarId == id);
        }
    }
}
