using System.Collections.Generic;
using vue_experiments.Data;
using vue_experiments.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace vue_experiments.Services
{
    public interface ICoffeeService
    {
        int SaveChanges();
        IEnumerable<Coffee> GetAll();
        Coffee Get(int id);
        Coffee Add(Coffee newCoffee);
        Coffee Update(Coffee updatedCoffee);
        void Delete(int id);
    }

    public class CoffeeService : ICoffeeService
    {
        private readonly CoffeeDbContext _context;

        public CoffeeService(CoffeeDbContext context)
        {
            this._context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IEnumerable<Coffee> GetAll()
        {
            return _context.Coffees.OrderBy(c => c.Name);
        }

        public Coffee Get(int id)
        {
            return _context.Coffees.Find(id);
        }

        public Coffee Add(Coffee newCoffee)
        {
            _context.Add(newCoffee);
            return newCoffee;
        }

        public Coffee Update(Coffee updatedCoffee)
        {
            var entityState = _context.Attach(updatedCoffee);
            entityState.State = EntityState.Modified;
            return updatedCoffee;
        }

        public void Delete(int id)
        {
            var coffee = _context.Coffees.Find(id);
            if (coffee != null)
            {
                _context.Coffees.Remove(coffee);
            }

        }
    }
}