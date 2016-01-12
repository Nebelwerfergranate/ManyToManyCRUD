using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class SqlDriverRepository : IRepository<Driver>
    {
        private readonly MotoristContext _context;
        private bool _disposed = false;
        public SqlDriverRepository(MotoristContext context)
        {
            _context = context;
        }

        public IEnumerable<Driver> GetItems()
        {
            return _context.Drivers;
        }

        public Driver GetItem(int id)
        {
            return _context.Drivers.Find(id);
        }

        public void Create(Driver driver)
        {
            _context.Drivers.Add(driver);
        }

        public void Update(Driver driver)
        {
            _context.Entry(driver).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Driver driver = _context.Drivers.Find(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
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
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}