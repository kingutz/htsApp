using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using htsApp.Data;
using htsApp.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using htsApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace htsApp.Controllers
{
    [Authorize(Roles = "admin,analyst,dataentry,dataclerk")]
    public class DistrictHtsController : Controller
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext _context;

        public DistrictHtsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ILogger<ApplicationDbContext> logger,
            ICurrentUserService currentUserService)

        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

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
        [Authorize(Roles = "admin,analyst")]
        public async Task<IActionResult> Create([Bind("DistrictName,Description,ID")] District district)
        {
            if (ModelState.IsValid)
            {
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
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
        [Authorize(Roles = "admin,analyst")]
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
        [Authorize(Roles = "admin,analyst")]
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

        [HttpGet]
        public JsonResult CreateJson(string Prefix)
        {
            var dist = _context.district.Where(i => i.DistrictName.StartsWith(Prefix)).Select(g=> new {g.DistrictName }).ToList();
            return Json(dist);
        }


    }
}
