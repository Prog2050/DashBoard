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
using Web.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]

    public class SalesController : Controller
    {
        private readonly IUnitOfWork<Sales_H> _Sales_H;
        private readonly IUnitOfWork<Sales_B> _Sales_B;
        private readonly IUnitOfWork<Registration> _Registration;
        public SalesController(IUnitOfWork<Sales_H> Sales_H,
            IUnitOfWork<Sales_B> Sales_B, IUnitOfWork<Registration> Registration
            )
        {
            this._Sales_H = Sales_H;
            this._Sales_B = Sales_B;
            this._Registration = Registration;

        }

        // GET: Sales
        public IActionResult Index()
        {
            var Sales = new SalesViewModel
            {
              
            Sales_Invoices_H =_Sales_H.Entity.GetAll().Where((number) => number.Done == 'N').ToList(),
            Sales_Invoices_B = _Sales_B.Entity.GetAll().ToList(),
            Registration = _Registration.Entity.GetAll().ToList(),
            };

            return View(Sales);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Done(int id , double amount , DateTime DateTime , int userNo , double HD, int Bills_No)
        {
            Sales_H Sales = new Sales_H
            {
                Order_No  = id,
                Done = 'Y', 
                Amount = amount,
                DateTime = DateTime,
                User_No = userNo,
                HD = HD, Bills_No = Bills_No
            };

            _Sales_H.Entity.Update(Sales);
            _Sales_H.Save();
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order_Received(int id, double amount, DateTime DateTime, int userNo, double HD, int Bills_No)
        {
            Sales_H Sales = new Sales_H
            {
                Order_No = id,
                 Done = 'N',
                State = "R",
                Amount = amount,
                DateTime = DateTime,
                User_No = userNo,
                HD = HD,
                Bills_No = Bills_No
            };

            _Sales_H.Entity.Update(Sales);
            _Sales_H.Save();
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order_Close(int id, double amount, DateTime DateTime, int userNo, double HD, int Bills_No)
        {
            Sales_H Sales = new Sales_H
            {
                Order_No = id,
                //    Done = 'Y',
                State = "C",
                Amount = amount,
                DateTime = DateTime,
                User_No = userNo,
                HD = HD,
                Bills_No = Bills_No
            };

            _Sales_H.Entity.Update(Sales);
            _Sales_H.Save();
            return RedirectToAction(nameof(Index));

        }


        //    // GET: Sales/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var sales_H = await _context.Sales_Invoices_H
        //            .FirstOrDefaultAsync(m => m.Order_No == id);
        //        if (sales_H == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(sales_H);
        //    }

        //    // GET: Sales/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Sales/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Amount,DateTime,HD,Order_No")] Sales_H sales_H)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(sales_H);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(sales_H);
        //    }

        //    // GET: Sales/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var sales_H = await _context.Sales_Invoices_H.FindAsync(id);
        //        if (sales_H == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(sales_H);
        //    }

        //    // POST: Sales/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Amount,DateTime,HD,Order_No")] Sales_H sales_H)
        //    {
        //        if (id != sales_H.Order_No)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(sales_H);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!Sales_HExists(sales_H.Order_No))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(sales_H);
        //    }

        //    // GET: Sales/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var sales_H = await _context.Sales_Invoices_H
        //            .FirstOrDefaultAsync(m => m.Order_No == id);
        //        if (sales_H == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(sales_H);
        //    }

        //    // POST: Sales/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var sales_H = await _context.Sales_Invoices_H.FindAsync(id);
        //        _context.Sales_Invoices_H.Remove(sales_H);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool Sales_HExists(int id)
        //    {
        //        return _context.Sales_Invoices_H.Any(e => e.Order_No == id);
        //    }

    }
}
