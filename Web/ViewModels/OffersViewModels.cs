using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OffersViewModels
    {
        public int Offers_No { get; set; }
        [Required(ErrorMessage = "أدخــل الــعرض")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = " يـجـب ان يـكون عـدد الاحـرف اكـبـر من 5")]

        public string Offer { get; set; }
        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "أدخـــل الــصورة")]
        public IFormFile File { get; set; }

    }
}
