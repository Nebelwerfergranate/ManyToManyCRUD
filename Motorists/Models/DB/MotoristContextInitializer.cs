using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class MotoristContextInitializer : DropCreateDatabaseAlways<MotoristContext>
    {
        protected override void Seed(MotoristContext context)
        {
            Car zapor = new Car {Brand = "ЗАЗ", Cost = 100, Model = "965", Type = "sedan", Year = 1960, RegistrationNumber = "АХ1234КХ"};
            Car boomer = new Car { Brand = "BMW", Cost = 9200, Model = "E38 750i", Type = "sedan", Year = 1997, RegistrationNumber = "АХ4321КХ"};
            Car volga = new Car {Brand = "ГАЗ", Cost = 500, Model = "24-02", Type = "sedan", Year = 1980, RegistrationNumber = "АХ2134КХ"};

            Driver biba = new Driver{FirstName = "Bibа", LastName = "Smith", Vatin = "1234567890" ,BirthYear = 1990, DrivingExperience = 2};
            Driver boba = new Driver { FirstName = "Bobа", LastName = "Johnson", Vatin = "0987654321", BirthYear = 1991, DrivingExperience = 5};
            Driver person = new Driver { FirstName = "Василий", LastName = "Иванов", Vatin = "5432167890" ,BirthYear = 1960, DrivingExperience = 20};

            context.Cars.AddRange(new List<Car> {zapor, boomer, volga});
            context.Drivers.AddRange(new List<Driver> {biba, boba, person});

            zapor.Owners = new List<Driver>{biba, boba};
            volga.Owners = new List<Driver>{person};
            context.SaveChanges();

            base.Seed(context);
        }
    }
}