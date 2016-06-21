using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please, enter your name!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Enter address")]
        [Display(Name="First Address Line")]
        public string Line1 { get; set; }
        [Display(Name = "Second Address Line")]
        public string Line2 { get; set; }
        [Display(Name = "Third Address Line")]
        public string Line3 { get; set; }

        [Required(ErrorMessage ="Enter City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter country!")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}