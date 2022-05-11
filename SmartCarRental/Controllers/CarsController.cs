using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBook.Helpers;
using SmartCarRental.Data;
using SmartCarRental.Models;
using SmartCarRental.ViewModels.Cars;
using SmartCarRental.ViewModels.UserRents;

namespace SmartCarRental.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var applicationDbContext = _context.Cars.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "renter,admin")]
        public async Task<IActionResult> RenterIndex()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Cars.Include(c => c.User).Where(c =>c.UserId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            ViewData["cars"] = await _context.Cars.Include(c => c.User)
                .OrderBy(c => Guid.NewGuid())
                .Take(12)
                .ToListAsync();
            return View();
        }
        public async Task<IActionResult> Search(CarTest car)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

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

            var car = _context.Cars.Include(c => c.User).Where(c => c.Id == id).Select(c => new CarVM
            {
                Id = c.Id,
                Name = c.Name,
                Model = c.Model,
                FirstLocation = c.FirstLocation,
                SecondLocation = c.SecondLocation,
                Phone = c.User.PhoneNumber,
                Description = c.Description,
                AvailableFrom = c.AvailableFrom,
                ImageUrl = c.ImgaUrl
            }).FirstOrDefault();
            ViewData["Car"] = car;
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["CarRent"] = _context.UserRents.Find(new object[] { car.Id, currentUser.Id });
            return View(new UserRentVM { Id = car.Id });
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
        public async Task<IActionResult> Create(Car car, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(car);

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

            var pathToSave = Path.Combine("images", "cars");
            string image;
            try
            {
                image = await FileHelper.SaveFileToServer(file, pathToSave);
            }
            catch (Exception ex)
            {
                return BadRequest("You are tying to add a blog without an IMAGE!");
            }

            if (image == null)
                return BadRequest("You are tying to add a blog without an IMAGE!");

            newCar.ImgaUrl = image;
            _context.Add(newCar);
            await _context.SaveChangesAsync();
            if (User.IsInRole("renter"))
                return RedirectToAction(nameof(RenterIndex));
            return RedirectToAction(nameof(AdminIndex));
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
        public async Task<IActionResult> Edit(Car car, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", car.UserId);
                return View(car);
            }
            if (file != null)
            {
                var pathToSave = Path.Combine("images", "blogs");
                string image;
                try
                {
                    image = await FileHelper.SaveFileToServer(file, pathToSave);
                }
                catch (Exception ex)
                {
                    return BadRequest("You are tying to add a blog without an IMAGE!");
                }

                if (image == null)
                    return BadRequest("You are tying to add a blog without an IMAGE!");

                car.ImgaUrl = image;
                _context.Update(car);
                await _context.SaveChangesAsync();
                if (User.IsInRole("renter"))
                    return RedirectToAction(nameof(RenterIndex));
                return RedirectToAction(nameof(AdminIndex));
            }

            var imageUrl = await _context.Cars.Where(c => c.Id == car.Id).Select(c => c.ImgaUrl).FirstOrDefaultAsync();
            car.ImgaUrl = imageUrl;
            _context.Update(car);
            await _context.SaveChangesAsync();

            if (User.IsInRole("renter"))
                return RedirectToAction(nameof(RenterIndex));
            return RedirectToAction(nameof(AdminIndex));
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
        [HttpPost]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var result = false;

            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                result = true;
            }
            var url = User.IsInRole("renter") ? "/Cars/RenterIndex" : "/Cars/AdminIndex";
            return Json(new { result, url });
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
