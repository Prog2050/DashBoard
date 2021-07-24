using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class SalesViewModel
    {
        public double Amount { get; set; }
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; }
        public double HD { get; set; }
        public int Order_No { get; set; }
        public char Done { get; set; }
        public int User_No { get; set; }
        public int Bills_No { get; set; }
        public List<Sales_H> Sales_Invoices_H { get; set; }
        public List<Sales_B> Sales_Invoices_B { get; set; }
        public List<Registration> Registration { get; set; }

    }
}
