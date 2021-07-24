using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly IWebHostEnvironment _hosting;
        private readonly IUnitOfWork<Category> _Category;

        public CategoriesController(IUnitOfWork<Category> Category, IWebHostEnvironment hosting)
        {
            _Category = Category;
            _hosting = hosting;
        }
        // GET: Categories

        public IActionResult Index()
        {
            return View(_Category.Entity.GetAll());
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = _Category.Entity.GetById(id);
            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModels model)
        {

          //  ModelState.Remove("ImageUrl");


            //if (ModelState.IsValid)
            //{

                try
                {
                  //  string fileName = UploadFile(model.File) ?? string.Empty;

                    //if (model.File != null)
                    //{
                    //    string uploads = Path.Combine(_hosting.WebRootPath, @"img\cat");
                    //    string fullPath = Path.Combine(uploads, model.File.FileName);
                    //    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    //}
                    Category Category = new Category
                    {
                        Cat_No = model.Cat_No,
                        Cat = model.Cat,
                    //    ImageUrl = fileName,

                    };

                    _Category.Entity.Insert(Category);
                    _Category.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    throw;
                }

            }

            //ModelState.AddModelError("", "الـبـيـانــات غــير صـحـيـحة");
            //return View(model);

        //}

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _Category.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            CategoryViewModels Category = new CategoryViewModels
            {
                Cat_No = portfolioItem.Cat_No,
                Cat = portfolioItem.Cat,
              //  ImageUrl = portfolioItem.ImageUrl,
            };

            return View(Category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryViewModels model)
        {
            try
            {
                // string fileName = UploadFile(model.File, model.ImageUrl);
                Category Category = new Category
                {
                    Cat_No = model.Cat_No,
                    Cat = model.Cat,
                    //  ImageUrl = fileName,
                };

                _Category.Entity.Update(Category);
                _Category.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioItemExists(model.Cat_No))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            //if (id != model.Cat_No)
            //{
            //    return NotFound();
            //}
            //ModelState.Remove("File");
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //       // string fileName = UploadFile(model.File, model.ImageUrl);
            //        Category Category = new Category
            //        {
            //            Cat_No = model.Cat_No,
            //            Cat = model.Cat,
            //          //  ImageUrl = fileName,
            //        };

            //        _Category.Entity.Update(Category);
            //        _Category.Save();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PortfolioItemExists(model.Cat_No))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(model);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _Category.Entity.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _Category.Entity.GetById(id);
            string uploads = Path.Combine(_hosting.WebRootPath, "img/cat");
            string OldPath = Path.Combine(uploads, category.ImageUrl);
            System.IO.File.Delete(OldPath);
            // fs.Close();
            //
            _Category.Entity.Delete(id);
            _Category.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _Category.Entity.GetAll().Any(e => e.Cat_No == id);
        }

        string UploadFile(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, "img/cat");
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
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }
        string UploadFile(IFormFile file, string imageUrl)
        {
            try
            {
                if (file != null)
                {

                    if (imageUrl == null)
                    {
                        UploadFile(file);
                        return file.FileName;
                    }
                    //
                    string uploads = Path.Combine(_hosting.WebRootPath, "img/cat");
                    string NewPath = Path.Combine(uploads, file.FileName);
                    string OldPath = Path.Combine(uploads, imageUrl);


                    if (OldPath != NewPath)
                    {

                        using FileStream fs = new FileStream(NewPath, FileMode.Create);
                        file.CopyTo(fs);
                        System.IO.File.Delete(OldPath);
                        // fs.Close();



                        //Save the New File 
                        //file.CopyTo(new FileStream(NewPath, FileMode.Create));



                    }




                    return file.FileName;

                }
                return imageUrl;
            }
            catch (Exception ex)
            {
                return ex.ToString();

            }

        }


        public ActionResult Search(string term)
        {
            if (term == null)
            {
                return RedirectPermanent("index");
            }
           
             var cat =_Category.Entity.GetAll().Where((Search) => Search.Cat.Contains(term)).ToList();
              
        

            return View("Search", cat);
        }
    }
}
