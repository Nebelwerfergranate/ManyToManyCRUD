using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly MotoristContext _context;
        private SqlDriverRepository _drivers;
        private SqlCarRepository _cars;
        private bool _disposed = false;

        public SqlUnitOfWork()
        {
            _context = new MotoristContext();
        }

        public IRepository<Driver> Drivers
        {
            get
            {
                if (_drivers == null)
                {
                    _drivers = new SqlDriverRepository(_context);
                }
                return _drivers;
            }
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (_cars == null)
                {
                    _cars = new SqlCarRepository(_context);
                }
                return _cars;
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