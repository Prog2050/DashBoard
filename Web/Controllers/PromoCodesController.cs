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
    public class PromoCodesController : Controller
    {
        private readonly IUnitOfWork<PromoCode> _PromoCode;
        public PromoCodesController(IUnitOfWork<PromoCode> PromoCode)
        {
            _PromoCode = PromoCode;
 
        }

        public IActionResult Index()
        {
            return View(_PromoCode.Entity.GetAll());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PromoCode = _PromoCode.Entity.GetById(id);
            if (PromoCode == null)
            {
                return NotFound();
            }

            return View(PromoCode);
        }
        public IActionResult Create()
        {
            return View();
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PromoCodeViewModel model)
        {

            if (ModelState.IsValid)
            {

    
                PromoCode PromoCode = new PromoCode
                {
                    ID = model.ID,
                    Code = model.Code,
                    Discount = model.Discount,
               
                };

                _PromoCode.Entity.Insert(PromoCode);
                _PromoCode.Save();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "الـبـيـانــات غــير صـحـيـحة");

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Get_PromoCode = _PromoCode.Entity.GetById(id);
            if (Get_PromoCode == null)
            {
                return NotFound();
            }

            PromoCodeViewModel Promo = new PromoCodeViewModel
            {
                ID = Get_PromoCode.ID,
                Code = Get_PromoCode.Code,
                Discount = Get_PromoCode.Discount,
            };

            return View(Promo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PromoCodeViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    PromoCode offersView = new PromoCode
                    {
                        ID = model.ID,
                        Code = model.Code,
                        Discount = model.Discount,
                    };

                    _PromoCode.Entity.Update(offersView);
                    _PromoCode.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteCode = _PromoCode.Entity.GetById(id);
            if (deleteCode == null)
            {
                return NotFound();
            }

            return View(deleteCode);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _PromoCode.Entity.GetById(id);
            _PromoCode.Entity.Delete(id);
            _PromoCode.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool PortfolioItemExists(int id)
        {
            return _PromoCode.Entity.GetAll().Any(e => e.ID == id);
        }

    }
}
