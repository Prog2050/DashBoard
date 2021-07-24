using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]

    public class OffersController : Controller
    {
        private readonly IWebHostEnvironment _hosting;
        private readonly IUnitOfWork<Offers> _Offers;

        public OffersController(IUnitOfWork<Offers> Offers, IWebHostEnvironment hosting)
        {
            _Offers = Offers;
            _hosting = hosting;
        }

        // GET: Offers
        public IActionResult Index()
        {
            return View(_Offers.Entity.GetAll().Where((number) => number.Offers_No!= 0));
            
        }

        // GET: Offers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Offers = _Offers.Entity.GetById(id);
            if (Offers == null)
            {
                return NotFound();
            }

            return View(Offers);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OffersViewModels model)
        {
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                string fileName = UploadFile(model.File) ?? string.Empty;

                //if (model.File != null)
                //{
                //    string uploads = Path.Combine(_hosting.WebRootPath, @"img\offers");
                //    string fullPath = Path.Combine(uploads, model.File.FileName);
                //    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                //}
                Offers Offers = new Offers
                {
                    Offers_No = model.Offers_No,
                    Offer = model.Offer,
                    ImageUrl = fileName,
                };

                _Offers.Entity.Insert(Offers);
                _Offers.Save();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "الـبـيـانــات غــير صـحـيـحة");

            return View(model);
        }

        // GET: Offers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Get_Offer = _Offers.Entity.GetById(id);
            if (Get_Offer == null)
            {
                return NotFound();
            }

            OffersViewModels Offers = new OffersViewModels
            {
                Offers_No = Get_Offer.Offers_No,
                Offer = Get_Offer.Offer,
                ImageUrl = Get_Offer.ImageUrl,
            };

            return View(Offers);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OffersViewModels model)
        {
            if (id != model.Offers_No)
            {
                return NotFound();
            }
            ModelState.Remove("File");

            if (ModelState.IsValid)
           {
                try
                {
                    //if (model.File != null)
                    //{
                    //    string uploads = Path.Combine(_hosting.WebRootPath, @"img\offers");
                    //    string fullPath = Path.Combine(uploads, model.Offers_No + model.File.FileName);
                    //    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    //}
                    string fileName = UploadFile(model.File, model.ImageUrl);

                    Offers offersView = new Offers
                    {
                        Offers_No = model.Offers_No,
                        Offer = model.Offer,
                        ImageUrl = fileName,
                    };

                    _Offers.Entity.Update(offersView);
                    _Offers.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Offers_No))
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

        // GET: Offers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteoffer = _Offers.Entity.GetById(id);
            if (deleteoffer == null)
            {
                return NotFound();
            }

            return View(deleteoffer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _Offers.Entity.GetById(id);
            string uploads = Path.Combine(_hosting.WebRootPath, "img/offers");
            string OldPath = Path.Combine(uploads, category.ImageUrl);
            System.IO.File.Delete(OldPath);
            //
            _Offers.Entity.Delete(id);
            _Offers.Save();
            return RedirectToAction(nameof(Index));
        }


        private bool PortfolioItemExists(int id)
        {
            return _Offers.Entity.GetAll().Any(e => e.Offers_No == id);
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, "img/offers");
                string fullPath = Path.Combine(uploads, file.FileName);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                {

                    file.CopyTo(fs);
                    //fs.Close();
                }
                return file.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                if(imageUrl == null)
                {
                    UploadFile(file);
                    return file.FileName;
                }

                string uploads = Path.Combine(_hosting.WebRootPath, "img/offers");
                string NewPath = Path.Combine(uploads, file.FileName);
                string OldPath = Path.Combine(uploads, imageUrl);
                if (OldPath != NewPath)
                {
                    using FileStream fs = new FileStream(NewPath, FileMode.Create);
                    file.CopyTo(fs);
                    System.IO.File.Delete(OldPath);
                    // fs.Close();
                }
                return file.FileName;

            }
            return imageUrl;

        }
        public ActionResult Search(string term)
        {
            if (term == null)
            {
                return RedirectPermanent("index");
            }

            var cat = _Offers.Entity.GetAll().Where((Search) => Search.Offer.Contains(term)).ToList();



            return View("Search", cat);
        }
    }
}
