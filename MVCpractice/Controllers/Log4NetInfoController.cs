using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCpractice.Models;

namespace MVCpractice.Controllers
{
    public class Log4NetInfoController : Controller
    {
        private readonly CRMContext _context;

        public Log4NetInfoController(CRMContext context)
        {
            _context = context;
        }

        // GET: Log4NetInfo
        public async Task<IActionResult> Index()
        {
              return View(await _context.Log4NetInfos.ToListAsync());
        }

        // GET: Log4NetInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Log4NetInfos == null)
            {
                return NotFound();
            }

            var log4NetInfo = await _context.Log4NetInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (log4NetInfo == null)
            {
                return NotFound();
            }

            return View(log4NetInfo);
        }

        // GET: Log4NetInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Log4NetInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Info")] Log4NetInfo log4NetInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(log4NetInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(log4NetInfo);
        }

        // GET: Log4NetInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Log4NetInfos == null)
            {
                return NotFound();
            }

            var log4NetInfo = await _context.Log4NetInfos.FindAsync(id);
            if (log4NetInfo == null)
            {
                return NotFound();
            }
            return View(log4NetInfo);
        }

        // POST: Log4NetInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Info")] Log4NetInfo log4NetInfo)
        {
            if (id != log4NetInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(log4NetInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Log4NetInfoExists(log4NetInfo.Id))
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
            return View(log4NetInfo);
        }

        // GET: Log4NetInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Log4NetInfos == null)
            {
                return NotFound();
            }

            var log4NetInfo = await _context.Log4NetInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (log4NetInfo == null)
            {
                return NotFound();
            }

            return View(log4NetInfo);
        }

        // POST: Log4NetInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Log4NetInfos == null)
            {
                return Problem("Entity set 'CRMContext.Log4NetInfos'  is null.");
            }
            var log4NetInfo = await _context.Log4NetInfos.FindAsync(id);
            if (log4NetInfo != null)
            {
                _context.Log4NetInfos.Remove(log4NetInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Log4NetInfoExists(int id)
        {
          return _context.Log4NetInfos.Any(e => e.Id == id);
        }
    }
}
