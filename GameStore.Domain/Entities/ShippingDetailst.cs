using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GameStore.Domain.Entities
{
    public class ShippingDetailst
    {
        [Required(ErrorMessage = "Please, enter your name!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Enter address")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage ="Enter City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter country!")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}