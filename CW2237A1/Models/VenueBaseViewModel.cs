using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW2237A1.Models
{
    public class VenueBaseViewModel : VenueAddViewModel
    {
        [Display(Name = "ID")]
        [Key]
        public int VenueId { get; set; }

        [Display(Name = "Venue Age")]
        public string YearsOld
        {
            get
            {
                if (OpenDate.HasValue)
                {
                    var age = Math.Floor((DateTime.Now - OpenDate.Value).TotalDays / 356.0);

                    if (age < 1.0)
                    {
                        return "Recently opened";
                    } 
                    else
                    {
                        return $"{age:n0} years old";
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}