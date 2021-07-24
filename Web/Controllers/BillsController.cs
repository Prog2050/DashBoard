using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]

    public class BillsController : Controller
    {
        private readonly IUnitOfWork<Sales_H> _Sales_H;
        private readonly IUnitOfWork<Sales_B> _Sales_B;
        private readonly IUnitOfWork<Registration> _Registration;
        public BillsController(IUnitOfWork<Sales_H> Sales_H,
            IUnitOfWork<Sales_B> Sales_B, IUnitOfWork<Registration> Registration
            )
        {
            this._Sales_H = Sales_H;
            this._Sales_B = Sales_B;
            this._Registration = Registration;

        }

        public IActionResult Index()
        {
            var Sales = new SalesViewModel
            {
                Sales_Invoices_H = _Sales_H.Entity.GetAll().Where((number) => number.Done == 'Y').ToList(),
                Sales_Invoices_B = _Sales_B.Entity.GetAll().ToList(),
                Registration = _Registration.Entity.GetAll().ToList(),
            };

            return View(Sales);
        }

        public IActionResult About()
        {
            return View();
        }
        public ActionResult Search(string term)
        {
            if (term == null)
            {
                return RedirectPermanent("index");
            }

            //var Bills = _Sales_H.Entity.GetAll().Where(p => p.Registration.UserName.ToLower().Contains(term) ||
            //     p.Registration.Phone.ToLower().Contains(term)).ToList();

            var Sales = new SalesViewModel
            {
                Sales_Invoices_H = _Sales_H.Entity.GetAll().Where((number) => number.Done == 'Y' && number.Registration.Phone==(term)).ToList(),
                Sales_Invoices_B = _Sales_B.Entity.GetAll().ToList(),
                Registration = _Registration.Entity.GetAll().ToList(),
            };
            //var Bills = _Sales_H.Entity.GetAll().Where(p => 
            //    p.Registration.Phone.ToLower().Contains(term)).ToList();
            return View("Search", Sales);
        }


    }
}