using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Motorists.Models 
{
    public class SqlCarRepository: IRepository<Car>
    {
        private readonly MotoristContext _context;
        private bool _disposed = false;
        public SqlCarRepository(MotoristContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetItems()
        {
            return _context.Cars;
        }

        public Car GetItem(int id)
        {
            return _context.Cars.Find(id);
        }

        public void Create(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Update(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Car car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}