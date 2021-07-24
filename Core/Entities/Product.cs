using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Items_No { get; set; }
        public string Items { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Code { get; set; } // Product Code 
        public double Old_Price { get; set; }
        [ForeignKey("ID_Offer")] // this make ForeignKey
        public Offers Offers { get; set; }
        [ForeignKey("ID_CAT")]  // this make ForeignKey
        public Category Category { get; set; }
        



    }
}
