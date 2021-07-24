using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CategoryViewModels
    {

        public int Cat_No { get; set; }

        [Required(ErrorMessage = "ادخل الـقـسم")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = " يـجـب ان يـكون عـدد الاحـرف اكـبـر من 5")]
        public string Cat { get; set; }
        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public string ImageUrl { get; set; }
        //DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public IFormFile File { get; set; }
    }
}
