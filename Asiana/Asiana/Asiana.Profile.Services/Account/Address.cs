using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Asiana.Profile.Services.Account
{
    public class Address
    {
        [ScaffoldColumn(false)]
        public long AddressID { get; set; }
        [ScaffoldColumn(false)]
        public string CustomerID { get; set; }
        [Display(Name="Address Name")]
        public string Name { get; set; }
        [Display(Name="Address")]
        public string AddressLineOne { get; set; }
        [Display(Name="Adddress")]
        public string AddressLineTwo { get; set; }
        [Display(Name="Town or City")]
        public string Town { get; set; }
        [Display(Name="Country")]
        public string County { get; set; }
        [Display(Name="Post Code")]
        public string PostCode { get; set; }
        [Display(Name="Country")]
        public string Country { get; set; }
        [ScaffoldColumn(false)]
        public string CountryCode { get; set; }
    }
}
