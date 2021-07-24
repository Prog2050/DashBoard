using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class PromoCode
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }

    }
}
