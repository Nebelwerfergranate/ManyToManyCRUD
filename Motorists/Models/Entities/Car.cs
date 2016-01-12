using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class Car
    {
        public Car() { }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        [Range(1900, 2016)]
        public int Year { get; set; }
        [RegularExpression(@"^[A-z А-я 0-9 -]{3,8}$")]
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }
        public virtual ICollection<Driver> Owners { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Brand, Model, Year);
        }
    }
}