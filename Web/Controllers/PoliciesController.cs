using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly IUnitOfWork<Policies> _Policies;
        public PoliciesController(IUnitOfWork<Policies> Policies)
        {
            _Policies = Policies;

        }
        public IActionResult Index()
        {
            return View(_Policies.Entity.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Get_PromoCode = _Policies.Entity.GetById(id);
            if (Get_PromoCode == null)
            {
                return NotFound();
            }

            PoliciesViewModels Policies = new PoliciesViewModels
            {
                ID = Get_PromoCode.ID,
              HD = Get_PromoCode.HD
            };

            return View(Policies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PoliciesViewModels model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    Policies offersView = new Policies
                    {
                        ID = model.ID,
                        HD = model.HD,
                     
                    };

                    _Policies.Entity.Update(offersView);
                    _Policies.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    //if (!PortfolioItemExists(model.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
