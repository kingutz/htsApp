using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using htsApp.Data;
using htsApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using htsApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace htsApp.Controllers
{
    [Authorize(Roles = "admin,analyst,dataentry,dataclerk")]
    public class FacilityHtsController : Controller
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext _context;

        public FacilityHtsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ILogger<ApplicationDbContext> logger,
            ICurrentUserService currentUserService)

        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        }

        // GET: FacilityHts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facility.ToListAsync());
        }

        // GET: FacilityHts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility
                .FirstOrDefaultAsync(m => m.ID == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // GET: FacilityHts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacilityHts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,analyst")]
        public async Task<IActionResult> Create([Bind("Id,FacilityName,Description")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        // GET: FacilityHts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return View(facility);
        }

        // POST: FacilityHts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,analyst")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FacilityName,Description")] Facility facility)
        {
            if (id != facility.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityExists(facility.ID))
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
            return View(facility);
        }

        // GET: FacilityHts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility
                .FirstOrDefaultAsync(m => m.ID == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: FacilityHts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,analyst")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var facility = await _context.Facility.FindAsync(id);
            _context.Facility.Remove(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(long id)
        {
            return _context.Facility.Any(e => e.ID == id);
        }
    }
}
