using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities
{
    public class Registration
    {
        [Key]
        public int User_No { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime Date { get; set; }
    }
}
