using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp1_restaurant.Models;

namespace tp1_restaurant.Data
{
    public class ZhaoContext : DbContext
    {
        public ZhaoContext(DbContextOptions<ZhaoContext> options) : base(options)
        {

        }

        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evaluation>().ToTable("Evaluations");
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<Promotion>().ToTable("Promotions");
        }
    }
}
