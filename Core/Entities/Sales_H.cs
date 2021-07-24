using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Sales_H
    {
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; } 
        public double HD { get; set; }
        [Key]
        public int Order_No { get; set; }
        public int User_No { get; set; }
      
        public int Bills_No { get; set; }
        public char Done { get; set; }
        public string State { get; set; }  
        [ForeignKey("User_No")] // this make ForeignKey
        public Registration Registration { get; set; }
        [ForeignKey("Order_No")]  // this make ForeignKey
        public Sales_B Sales_Invoices_B { get; set; }

    }
}
