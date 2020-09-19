using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using htsApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace htsApp.Controllers
{
    [Route("api/facilities")]
    [ApiController]
    public class FacilityRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacilityRestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("searchfac")]
        public async Task<IActionResult> Facilitysrc()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = await _context.Facility.Where(p => p.FacilityName.Contains(term)).Select(p => p.FacilityName).ToListAsync();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
