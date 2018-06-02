using Microsoft.EntityFrameworkCore;
using vue_experiments.Models;

namespace vue_experiments.Data
{
    public class CoffeeDbContext : DbContext
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<Coffee> Coffees { get; set; }
    }
}