using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorists.Models
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Driver> Drivers { get; }
        IRepository<Car> Cars { get; }
        void Save();
    }
}
