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
    public class HTSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HTSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HTS
        public async Task<IActionResult> Index()
        {
            return View(await _context.HTS.ToListAsync());
        }

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: HTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameofSite,OutreachName,District,Month,Year,OutreachOrganizer,PublicData,PrivateData,CSO,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus,ResidenceShehia,ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling,Reasonfortesting,Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued,Nooffemalecondomsissued,Referredto,CTCNumber,ID")] HTSData hTSData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hTSData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(long id, [Bind("NameofSite,OutreachName,District,Month,Year,OutreachOrganizer,PublicData,PrivateData,CSO,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus,ResidenceShehia,ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling,Reasonfortesting,Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued,Nooffemalecondomsissued,Referredto,CTCNumber,ID")] HTSData hTSData)
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
