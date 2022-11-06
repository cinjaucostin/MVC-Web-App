using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCpractice.Models;
using MVCpractice.ViewModels;

namespace MVCpractice.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly CRMContext db;

        const int perPage = 10;
        public SuppliersController(CRMContext db)
        {
            this.db = db;
        }

        // GET: Suppliers
        public IActionResult Index(int page = 1, string sortField = "", bool ascending = true)
        {
            SupplierIndexVM model = new SupplierIndexVM();

            IQueryable<Supplier> suppliers = db.Suppliers;

            model.CurrentPage = page;
            model.Ascending = ascending;
            model.SortField = sortField;

            model.CurrentField = sortField;

            model.Records = db.Suppliers.Count();
            model.Pages = (model.Records % perPage == 0) ?
                                            (model.Records / perPage) :
                                            (model.Records / perPage) + 1;

            switch (sortField)
            {
                case "CompanyName":
                    suppliers = ascending ? suppliers.OrderBy(s => s.CompanyName) :
                                            suppliers.OrderByDescending(s => s.CompanyName);
                    break;
                case "ContactName":
                    suppliers = ascending ? suppliers.OrderBy(s => s.ContactName) :
                                            suppliers.OrderByDescending(s => s.ContactName);
                    break;
                case "City":
                    suppliers = ascending ? suppliers.OrderBy(s => s.City) :
                                            suppliers.OrderByDescending(s => s.City);
                    break;
                case "Country":
                    suppliers = ascending ? suppliers.OrderBy(s => s.Country) :
                                            suppliers.OrderByDescending(s => s.Country);
                    break;
                default:
                    suppliers = db.Suppliers;
                    break;

            }

            model.Suppliers = suppliers.Skip((page - 1) * perPage).Take(perPage).ToList();

            return View(model);
        }


        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await db.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Add(supplier);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(supplier);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await db.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Suppliers == null)
            {
                return Problem("Entity set 'CRMContext.Suppliers'  is null.");
            }
            var supplier = await db.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                db.Suppliers.Remove(supplier);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
          return db.Suppliers.Any(e => e.Id == id);
        }
    }
}
