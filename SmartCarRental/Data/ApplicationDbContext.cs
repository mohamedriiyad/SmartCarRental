using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCarRental.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCarRental.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CarRent>()
                .HasKey(cr => new { cr.CarId, cr.RenterId });
            builder.Entity<CarRent>()
                .HasOne(bc => bc.Renter)
                .WithMany(b => b.CarRents)
                .HasForeignKey(bc => bc.RenterId);
            builder.Entity<CarRent>()
                .HasOne(bc => bc.Car)
                .WithMany(c => c.CarRents)
                .HasForeignKey(bc => bc.CarId);

            builder.Entity<UserRent>()
                .HasKey(cr => new { cr.CarId, cr.UserId });
            builder.Entity<UserRent>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRents)
                .HasForeignKey(bc => bc.UserId);
            builder.Entity<UserRent>()
                .HasOne(bc => bc.Car)
                .WithMany(c => c.UserRents)
                .HasForeignKey(bc => bc.CarId);

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<UserRent> UserRents { get; set; }
        public DbSet<CarRent> CarRents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
