using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Offers
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Offers_No { get; set; }
        public string Offer { get; set; }
        public string ImageUrl { get; set; }
    }
}
