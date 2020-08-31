using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using htsApp.Data;
using htsApp.Models;

namespace htsApp.Controllers
{
    public class ShehiaHtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShehiaHtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShehiaHts
        public async Task<IActionResult> Index()
        {
            return View(await _context.shehia.ToListAsync());
        }

        // GET: ShehiaHts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shehia = await _context.shehia
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shehia == null)
            {
                return NotFound();
            }

            return View(shehia);
        }

        // GET: ShehiaHts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShehiaHts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("districtId,ShehiaName,Description,ID")] Shehia shehia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shehia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shehia);
        }

        // GET: ShehiaHts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shehia = await _context.shehia.FindAsync(id);
            if (shehia == null)
            {
                return NotFound();
            }
            return View(shehia);
        }

        // POST: ShehiaHts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("districtId,ShehiaName,Description,ID")] Shehia shehia)
        {
            if (id != shehia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shehia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShehiaExists(shehia.ID))
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
            return View(shehia);
        }

        // GET: ShehiaHts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shehia = await _context.shehia
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shehia == null)
            {
                return NotFound();
            }

            return View(shehia);
        }

        // POST: ShehiaHts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var shehia = await _context.shehia.FindAsync(id);
            _context.shehia.Remove(shehia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShehiaExists(long id)
        {
            return _context.shehia.Any(e => e.ID == id);
        }
    }
}
