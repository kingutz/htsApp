using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using htsApp.Data;
using htsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using htsApp.Services;

namespace htsApp.Controllers
{
    [Authorize(Roles = "admin,analyst,dataentry,dataclerk")]
    public class HTSController : Controller
    {
        
        private readonly ILogger<ApplicationDbContext> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext _context;

        public HTSController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ILogger<ApplicationDbContext> logger, 
            ICurrentUserService currentUserService)
       
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        }
        private void PopulateDistrictDropDownList(object selectedNumber = null)
        {
            var dsnQuery = from d in _context.district
                                       orderby d.DistrictName
                                       select new {d.DistrictName };
            ViewBag.dsn = new SelectList(dsnQuery, "DistrictName", "DistrictName", selectedNumber);
        }
        private void PopulateFacilityDropDownList(object selectedNumber = null)
        {
            var dsnQuery = from d in _context.Facility
                           orderby d.FacilityName
                           select new { d.FacilityName };
            ViewBag.dsn = new SelectList(dsnQuery, "FacilityName", "FacilityName", selectedNumber);
        }
        private void PopulateShehiaDropDownList(object selectedNumber = null)
        {
            var dsnQuery = from d in _context.shehia
                           orderby d.ShehiaName
                           select new { d.ShehiaName };
            ViewBag.dsn = new SelectList(dsnQuery, "ShehiaName", "ShehiaName", selectedNumber);
        }
        // GET: HTS
        public async Task<IActionResult> Index()
        {
            bool isAdmin = User.IsInRole("admin") || User.IsInRole("analyst");

            if (isAdmin)
            {
                
                return View(await _context.HTS.ToListAsync());
            }
            else

             return View(await _context.HTS.Where(p => p.CreatedByUser == currentUserService.GetCurrentUsername()).ToListAsync());

        }
            //return View(await _context.HTS.ToListAsync());
        

        // GET: HTS/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hTSData = await _context.HTS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hTSData == null)
            {
                return NotFound();
            }

            return View(hTSData);
        }

        // GET: HTS/Create
        public async Task<IActionResult> Create()
        //public IActionResult Create()
        {
            PopulateDistrictDropDownList();
            //PopulateFacilityDropDownList();
            var hTSData = await _context.HTS.OrderByDescending(i=>i.ID).FirstOrDefaultAsync();
            if (hTSData == null)
            {
                return NotFound();
            }
            return View(hTSData);
            //return View();
        }

        // POST: HTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameofSite,OutreachName,District,Month,Year,OutreachOrganizer," +
            "PublicData,PrivateData,CSO,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus,ResidenceShehia," +
            "ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling,Reasonfortesting," +
            "Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued,Nooffemalecondomsissued," +
            "Referredto,CTCNumber,ID,RemarksName")] HTSData hTSData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hTSData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(hTSData);
        }

        // GET: HTS/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hTSData = await _context.HTS.FindAsync(id);
            if (hTSData == null)
            {
                return NotFound();
            }
            return View(hTSData);
        }

        // POST: HTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("NameofSite,OutreachName,District,Month,Year," +
            "OutreachOrganizer,PublicData,PrivateData,CSO,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus," +
            "ResidenceShehia,ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling," +
            "Reasonfortesting,Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued," +
            "Nooffemalecondomsissued,Referredto,CTCNumber,ID,RemarksName")] HTSData hTSData)
        {
            if (id != hTSData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hTSData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HTSDataExists(hTSData.ID))
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
            return View(hTSData);
        }

        // GET: HTS/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hTSData = await _context.HTS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hTSData == null)
            {
                return NotFound();
            }

            return View(hTSData);
        }

        // POST: HTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var hTSData = await _context.HTS.FindAsync(id);
            _context.HTS.Remove(hTSData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HTSDataExists(long id)
        {
            return _context.HTS.Any(e => e.ID == id);
        }
    }
}
