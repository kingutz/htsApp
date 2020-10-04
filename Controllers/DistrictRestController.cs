using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using htsApp.Data;
using htsApp.Models;

namespace htsApp.Controllers
{
    [Route("api/districts")]
    [ApiController]
    public class DistrictRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistrictRestController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Produces("application/json")]
        [HttpGet("searchdist")]
        public async Task<IActionResult> Districtsrc()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names =await _context.district.Where(p => p.DistrictName.Contains(term)).Select(p => p.DistrictName).ToListAsync();
                return  Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = await _context.district.Where(p => p.DistrictName.Contains(term)).Select(p => p.DistrictName).ToListAsync();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[Produces("application/json")]
        //[HttpGet("[id]")]
        //public async Task<IActionResult> DstShehia(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var term = HttpContext.Request.Query["term"];
        //    var hTSData = await _context.district.FirstOrDefaultAsync(m => m.ID == id);
        //    if (hTSData == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(hTSData);
        //}


        // GET: api/DistrictRest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> Getdistrict()
        {
            return await _context.district.ToListAsync();
        }

        // GET: api/DistrictRest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<District>> GetDistrict(long id)
        {
            var district = await _context.district.FindAsync(id);

            if (district == null)
            {
                return NotFound();
            }

            return district;
        }

        // PUT: api/DistrictRest/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(long id, District district)
        {
            if (id != district.ID)
            {
                return BadRequest();
            }

            _context.Entry(district).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DistrictRest
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<District>> PostDistrict(District district)
        {
            _context.district.Add(district);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrict", new { id = district.ID }, district);
        }

        // DELETE: api/DistrictRest/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<District>> DeleteDistrict(long id)
        {
            var district = await _context.district.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            _context.district.Remove(district);
            await _context.SaveChangesAsync();

            return district;
        }

        private bool DistrictExists(long id)
        {
            return _context.district.Any(e => e.ID == id);
        }
        
    }
}
