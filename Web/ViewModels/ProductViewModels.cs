using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ProductViewModels
    {
        public int Items_No { get; set; }




        [Required(ErrorMessage = "أدخــل الـصـنـف")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = " يـجـب ان يـكون عـدد الاحـرف اكـبـر من 2")]
        public string Items { get; set; }

        
        [Required(ErrorMessage = "أدخــل كـود الـصـنـف")]
        public string Code { get; set; }


        [Required(ErrorMessage = "أدخــل الـوصــف")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = " يـجـب ان يـكون عـدد الاحـرف اكـبـر من 2")]
        public string Description { get; set; }


        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public string ImageUrl { get; set; }



        [Required(ErrorMessage = "أدخـــل سعر الصـنف بعد الـعرض")]
        [Range(1, 10000, ErrorMessage = "أدخـــل سعر الصـنف بعد الـعرض")]
        public double Price { get; set; }



        //[Required(ErrorMessage = "أدخـــل سعر الصـنف قبل الـعرض")]
        //[Range(1, 10000, ErrorMessage = "أدخـــل سعر الصـنف قبل الـعرض")]
        //public double Old_Price { get; set; }


        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public IFormFile File { get; set; }


        //[Required(ErrorMessage = "أدخـــل الــقــسم")]
        [Range(1, 100000, ErrorMessage = "أدخـــل الــقــسم")]
        public int ID_CAT { get; set; }


        //[Required(ErrorMessage = "أدخـــل العــرض")]
      //  [Range(1, 100000, ErrorMessage = "أدخـــل العــرض")]
        public int ID_Offer { get; set; }
        public List<Offers> Offers { get; set; }
        public List<Category> Category { get; set; }
        public List<Product> Pro { get; set; }
    }
}
