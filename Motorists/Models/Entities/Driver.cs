using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class Driver
    {
        public Driver() { }

        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [RegularExpression(@"^\d{10}$")]
        [Display(Name = "VATIN")]
        public string Vatin { get; set; }
        [Range(1900, 2016)]
        [Display(Name = "Birth year")]
        public int BirthYear { get; set; }
        [Display(Name = "Driving experience")]
        public int DrivingExperience { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", FirstName, LastName, BirthYear);
        }
    }
}