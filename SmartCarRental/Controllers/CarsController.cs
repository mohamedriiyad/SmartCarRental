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
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CarsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cars
        public async Task<IActionResult> AdminIndex()
        {
            var applicationDbContext = _context.Cars.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            ViewData["cars"] = await _context.Cars.Include(c => c.User).ToListAsync();
            return View();
        }
        public async Task<IActionResult> Search(CarTest car)
        {
            var day = car.Available.Day;
            var month = car.Available.Month;
            var year = car.Available.Year;
            var applicationDbContext = await _context.Cars.Include(c => c.User)
                .Where(c => c.FirstLocation.Contains(car.Location) || c.SecondLocation.Contains(car.Location)).ToListAsync();

            var cars = applicationDbContext.Where(c =>
                (c.AvailableFrom.Day.Equals(day) &&
                c.AvailableFrom.Month.Equals(month) &&
                c.AvailableFrom.Year.Equals(year))).ToList();

            ViewData["cars"] = cars;
            return View("Index", car);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["CurrentUser"] = currentUser;
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,FirstLocation,SecondLocation,Description,AvailableFrom,UserId")] Car car)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewData["CurrentUser"] = currentUser;
                Car newCar = new Car
                {
                    Id = car.Id,
                    Name = car.Name,
                    Model = car.Model,
                    FirstLocation = car.FirstLocation,
                    SecondLocation = car.SecondLocation,
                    Description = car.Description,
                    AvailableFrom = car.AvailableFrom,
                    UserId = currentUser.Id
                };
                _context.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", car.UserId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,FirstLocation,SecondLocation,Description,AvailableFrom,UserId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", car.UserId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
