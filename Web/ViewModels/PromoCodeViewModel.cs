using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PromoCodeViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
    }
}
