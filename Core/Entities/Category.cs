using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Category
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Cat_No { get; set; }
        public string Cat { get; set; }
        public string ImageUrl { get; set; }
    }
}
