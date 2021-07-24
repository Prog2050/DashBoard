using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
   public class Policies
    {
        [Key]
        public int ID { get; set; }
        public double HD { get; set; }
    }
}
