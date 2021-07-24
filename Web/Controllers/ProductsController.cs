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

    public class ProductsController : Controller
    {
        private readonly IUnitOfWork<Product> _Product;
        private readonly IUnitOfWork<Category> _Category;
        private readonly IUnitOfWork<Offers> _Offers;
        private readonly IWebHostEnvironment _hosting;

        public ProductsController(IUnitOfWork<Product> Product,
           IUnitOfWork<Category> Category, IUnitOfWork<Offers> Offers,
            IWebHostEnvironment hosting)
        {
            this._Product = Product;
            this._Category = Category;
            this._Offers = Offers;
            this._hosting = hosting;
        }
        // GET: Products
        public IActionResult Index()
        {
            _Offers.Entity.GetAll();
            _Category.Entity.GetAll();
            var products = _Product.Entity.GetAll();
            return View(products);

        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var Category = _Product.Entity.GetById(id);
            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            try
            {
                var model = new ProductViewModels
                {
                    Category = FillSelectListCat(),
                   // Offers = FillSelectListOFFER()
                };
                return View(model);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModels model)
        {

            ModelState.Remove("ImageUrl");
            //if (ModelState.IsValid)
            //{

                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;

                    //if (model.ID_Offer == -1)
                    //{

                    //    ViewBag.Message = "أدخــل الــعرض";
                    //    return View(GetAll());
                    //}
                    if (model.ID_CAT == -1)
                    {

                        ViewBag.Message = "أدخــل الـقــسم";
                        return View(GetAll());
                    }
                    //if (model.Price >= model.Old_Price)
                    //{
                    //    ViewBag.Message = "سعر العرض اكبر من السعر قبل العرض";
                    //    return View(GetAll());
                    //}

                    var category = _Category.Entity.GetById(model.ID_CAT);
                  //  var offers = _Offers.Entity.GetById(model.ID_Offer);

                    Product Product = new Product
                    {
                        Items_No = model.Items_No,
                        Code = model.Code,
                        Items = model.Items,
                        Price = model.Price,
                        Description = model.Description,
                      //  Old_Price = model.Old_Price,
                        ImageUrl = fileName,
                       // Offers = offers,
                        Category = category,
                    };
                    _Product.Entity.Insert(Product);
                    _Product.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {


                }

            //}

            //ModelState.AddModelError("", "الـبـيـانــات غــير صـحـيـحة");
            return View(model);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            _Offers.Entity.GetAll();
            _Category.Entity.GetAll();
            var Product = _Product.Entity.GetById(id);
            //    var authorId = book.Author == null ? book.Author.Id = 1 : book.Author.Id;
        //    var Id_Offers = Product.Offers == null ? Product.Offers.Offers_No = 1 : Product.Offers.Offers_No;
            var Id_Cat = Product.Category == null ? Product.Category.Cat_No = 1 : Product.Category.Cat_No;
            var viewModel = new ProductViewModels
            {
                Items_No = Product.Items_No,Code = Product.Code,
                Description = Product.Description,
                ImageUrl = Product.ImageUrl,
                Items = Product.Items,
                //Old_Price = Product.Old_Price,
                Price = Product.Price,
                Category = _Category.Entity.GetAll().ToList(),
                Offers = _Offers.Entity.GetAll().ToList(),
               // ID_Offer = Id_Offers,
                ID_CAT = Id_Cat,
            };
            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModels viewModel)
        {
            ModelState.Remove("File");
            ModelState.Remove("ImageUrl");

            //if (ModelState.IsValid)
            //{
                try
                {
                    string fileName = UploadFile(viewModel.File, viewModel.ImageUrl);
                    var category = _Category.Entity.GetById(viewModel.ID_CAT);
                   // var offers = _Offers.Entity.GetById(viewModel.ID_Offer);
                    Product product = new Product
                    {
                        Items_No = viewModel.Items_No,Code = viewModel.Code,
                        Items = viewModel.Items,
                        ImageUrl = fileName,
                        Description = viewModel.Description,
                        Price = viewModel.Price,
                    //    Old_Price = viewModel.Old_Price,
                        Category = category,
                    //    Offers = offers,
                    };
                    _Product.Entity.Update(product);
                    _Product.Save();
                    // TODO: Add update logic here
                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                }
            //}

            //ModelState.AddModelError("", "الـبـيـانــات غــير صـحـيـحة");

            return View(GetAll());


        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _Product.Entity.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _Product.Entity.GetById(id);
            string uploads = Path.Combine(_hosting.WebRootPath, "img/product");
            string OldPath = Path.Combine(uploads, product.ImageUrl);
            System.IO.File.Delete(OldPath);
            // fs.Close();
            //
            //
            _Product.Entity.Delete(id);
            _Product.Save();
            return RedirectToAction(nameof(Index));
        }


        ProductViewModels GetAllCategory()
        {
            var vmodel = new ProductViewModels
            {
                Category = FillSelectListCat()
            };
            return vmodel;
        }
        ProductViewModels GetAllOffers()
        {
            var vmodel = new ProductViewModels
            {
                Offers = FillSelectListOFFER()
            };
            return vmodel;
        }
        ProductViewModels GetAll()
        {
            var vmodel = new ProductViewModels
            {
                Offers = FillSelectListOFFER(),
                Category = FillSelectListCat(),

            };
            return vmodel;
        }
        List<Category> FillSelectListCat()
        {
            var category = _Category.Entity.GetAll().ToList();
            category.Insert(0, new Category { Cat_No = -1, Cat = "--- الــقــسم ---" });
            return category;
        }
        List<Offers> FillSelectListOFFER()
        {
            var offer = _Offers.Entity.GetAll().ToList();
            offer.Insert(0, new Offers { Offers_No = -1, Offer = "--- الــعــرض ---" });
            return offer;
        }


        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, "img/product");
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
                if (imageUrl == null)
                {
                    UploadFile(file);
                    return file.FileName;
                }

                string uploads = Path.Combine(_hosting.WebRootPath, "img/product");
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
            var Pro = new ProductViewModels
            {
                Category = _Category.Entity.GetAll().ToList(),
                Offers = _Offers.Entity.GetAll().ToList(),
                //  Pro = _Product.Entity.GetAll().Where((Search) => Search.Items.Contains(term)).ToList() ,
                Pro = _Product.Entity.GetAll().Where(p => p.Items.ToLower().Contains(term) ||
                 p.Code.ToLower().Contains(term)).ToList(),

            };

            return View("Search", Pro);
        }
    }
}
