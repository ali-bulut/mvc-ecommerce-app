using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Models
{
    public class ShippingDetails
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Address Title")]
        public string AddressTitle { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string  Address { get; set; }

        [Required(ErrorMessage = "Please Enter City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Please Enter District")]
        public string District { get; set; }

        [Required(ErrorMessage = "Please Enter Post Code")]
        public string PostCode { get; set; }
    }
}