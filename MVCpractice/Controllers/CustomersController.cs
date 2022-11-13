using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCpractice.ActionFilter;
using MVCpractice.Models;
using MVCpractice.ViewModels;

namespace MVCpractice.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CRMContext db;

        const int perPage = 10;

        public CustomersController(CRMContext db)
        {
            this.db = db;
        }

        // GET: Customers
        public IActionResult Index(int page = 1, string sortField = "", bool ascending = true, string filterKey="")
        {
            CustomerIndexVM model = new CustomerIndexVM();

            model.CurrentPage = page;
            model.Ascending = ascending;
            model.SortField = sortField;

            model.FilterKey = filterKey;

            model.CurrentField = model.SortField;

            IQueryable<Customer> customers = db.Customers;

            customers = db.Customers.Where(c => c.FirstName.Contains(filterKey));

            model.Records = customers.Count();
            model.Pages = (model.Records % perPage == 0) ?
                                            (model.Records / perPage) :
                                            (model.Records / perPage) + 1;

            switch (sortField)
            {
                case "FirstName":
                    customers = ascending ? customers.OrderBy(c => c.FirstName) :
                                            customers.OrderByDescending(c => c.FirstName);
                    break;
                case "LastName":
                    customers = ascending ? customers.OrderBy(c => c.LastName) :
                                            customers.OrderByDescending(c => c.LastName);
                    break;
                case "City":
                    customers = ascending ? customers.OrderBy(c => c.City) :
                                            customers.OrderByDescending(c => c.City);
                    break;
                case "Country":
                    customers = ascending ? customers.OrderBy(c => c.Country) :
                                            customers.OrderByDescending(c => c.Country);
                    break;
                default:
                    break;
            }

            model.Customers = customers.Skip((page - 1) * perPage).Take(perPage).ToList();

            return View(model);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(customer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Customers == null)
            {
                return Problem("Entity set 'CRMContext.Customers'  is null.");
            }
            var customer = await db.Customers.FindAsync(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return db.Customers.Any(e => e.Id == id);
        }
    }
}
