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
    [Route("api/shehia")]
    [ApiController]
    public class ShehiaRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShehiaRestController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/DistrictRest/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Shehia>> GetDistrict(long id)
        //{
        //    var district = await _context.shehia.Where(i=>i.ID==id).Select(g=> new  { });

        //    if (district == null)
        //    {
        //        return NotFound();
        //    }

        //    return district;
        //}
        [Produces("application/json")]
        [HttpPost("searchshe")]
        public async Task<IActionResult> Shehiasrc()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
               var names = await _context.shehia.Where(p => p.ShehiaName.Contains(term)).Select(p => p.ShehiaName).ToListAsync();

           // var names = await _context.shehia.Where(p => p.ShehiaName.Contains(term)).
           //Select(p => new ShehiaView { Id=p.districtId,Name= p.ShehiaName }).ToListAsync();
                return Ok(names);
                //return Json(names, JsonRequestBehavior.AllowGet);
               
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
