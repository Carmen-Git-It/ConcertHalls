﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW2237A1.Models
{
    public class VenueAddViewModel
    {
        [Required]
        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(60)]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "Date Opened")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpenDate { get; set; }

        public VenueAddViewModel()
        {
            OpenDate = DateTime.Now.AddYears(-23);
        }
    }
}