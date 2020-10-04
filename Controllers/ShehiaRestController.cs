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
    [Route("api/shehiaa")]
    [ApiController]
    public class ShehiaRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistrictShehia _districtshehia;
        public ShehiaRestController(ApplicationDbContext context, IDistrictShehia districtShehia)
        {
            _context = context;
            _districtshehia = districtShehia;
        }
        [BindProperty(SupportsGet = true)]
        public long DId { get; set; }
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
        [HttpGet("searchshe")]
        public async Task<IActionResult> Shehiasrc()
        {
         
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = await _context.shehia.Where(p => p.ShehiaName.Contains(term)).Distinct().
                    Select(p => p.ShehiaName).ToListAsync();
                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }
       
        public JsonResult OnGetShehiaa(long DId)
       
        {
            return new JsonResult(_districtshehia.GetShehia(DId));
           
        }
        //[HttpGet("id")]
        public IActionResult Search(long id)
        {
            var result = _context.shehia.Where(d=>d.districtId==id).Select(d=> new Shehia {ShehiaName=d.ShehiaName }).ToList();
            if (!result.Any())
            {
                return NotFound(id);
            }
            return Ok(result);
        }
    }
}
