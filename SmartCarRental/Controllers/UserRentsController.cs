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
using SmartCarRental.ViewModels.UserRents;
using SmartCarRental.ViewModels.Users;

namespace SmartCarRental.Controllers
{
    public class UserRentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRentsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserRents
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = await _context.UserRents
                .Include(u => u.Car)
                .Include(u => u.User)
                .Where(u => u.UserId == currentUser.Id).Select(c => new CarVM
            {
                Id = c.Car.Id,
                Name = c.Car.Name,
                Model = c.Car.Model,
                FirstLocation = c.Car.FirstLocation,
                SecondLocation = c.Car.SecondLocation,
                Phone = c.User.PhoneNumber,
                Description = c.Car.Description,
                AvailableFrom = c.Car.AvailableFrom
            }).ToListAsync();
            return View(applicationDbContext);
        }

        // GET: UserRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRent = await _context.UserRents
                .Include(u => u.Car)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (userRent == null)
            {
                return NotFound();
            }

            return View(userRent);
        }

        // GET: UserRents/Create
        public IActionResult Create(int id)
        {
            var car = _context.Cars.Include(c => c.User).Select(c => new CarVM{ 
                Id = c.Id,
                Name = c.Name,
                Model = c.Model,
                FirstLocation = c.FirstLocation,
                SecondLocation = c.SecondLocation,
                Phone = c.User.PhoneNumber,
                Description = c.Description,
                AvailableFrom = c.AvailableFrom
            }).FirstOrDefault();
            ViewData["Car"] = car;
            return View(new UserRentVM { Id = car.Id});
        }

        // POST: UserRents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRent(UserRentVM input)
        {
            var car = _context.Cars.Find(input.Id);
            if (!ModelState.IsValid || car == null)
                return View();
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userRent = new UserRent { CarId = input.Id, UserId = currentUser.Id };
            if(_context.UserRents.Contains(userRent))
            {
                ModelState.AddModelError("", "This Car is Already Reserved!"); 
                var newCar = _context.Cars.Include(c => c.User).Where(c => c.Id == car.Id).Select(c => new CarVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Model = c.Model,
                    FirstLocation = c.FirstLocation,
                    SecondLocation = c.SecondLocation,
                    Phone = c.User.PhoneNumber,
                    Description = c.Description,
                    AvailableFrom = c.AvailableFrom
                }).FirstOrDefault();
                ViewData["Car"] = newCar;
                return View("Create", input);
            }

            _context.UserRents.Add(userRent);  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRent = await _context.UserRents.FindAsync(id);
            if (userRent == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description", userRent.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRent.UserId);
            return View(userRent);
        }

        // POST: UserRents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,CarId")] UserRent userRent)
        {
            if (id != userRent.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRentExists(userRent.CarId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description", userRent.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRent.UserId);
            return View(userRent);
        }

        // GET: UserRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRent = await _context.UserRents
                .Include(u => u.Car)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (userRent == null)
            {
                return NotFound();
            }

            return View(userRent);
        }

        // POST: UserRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRent = await _context.UserRents.FindAsync(id);
            _context.UserRents.Remove(userRent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRentExists(int id)
        {
            return _context.UserRents.Any(e => e.CarId == id);
        }
    }
}
