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
    public class DistrictHtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictHtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DistrictHts
        public async Task<IActionResult> Index()
        {
            return View(await _context.district.ToListAsync());
        }

        // GET: DistrictHts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _context.district
                .FirstOrDefaultAsync(m => m.ID == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // GET: DistrictHts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DistrictHts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictName,Description,ID")] District district)
        {
            if (ModelState.IsValid)
            {
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(district);
        }

        // GET: DistrictHts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _context.district.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        // POST: DistrictHts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("DistrictName,Description,ID")] District district)
        {
            if (id != district.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictExists(district.ID))
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
            return View(district);
        }

        // GET: DistrictHts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _context.district
                .FirstOrDefaultAsync(m => m.ID == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // POST: DistrictHts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var district = await _context.district.FindAsync(id);
            _context.district.Remove(district);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictExists(long id)
        {
            return _context.district.Any(e => e.ID == id);
        }
    }
}
