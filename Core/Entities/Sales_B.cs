using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Sales_B
    {
        public int ItemsNo { get; set; }
        public string Items { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
     //   [ForeignKey("Order_No")]
        public int Order_No { get; set; }
        public int keys { get; set; }
        public int User_No { get; set; }
    }
}
