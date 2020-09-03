using System.Linq;
using System.Threading.Tasks;
using htsApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Produces("application/json")]
        [HttpGet("searchshe")]
        public async Task<IActionResult> Shehiasrc()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = await _context.shehia.Where(p => p.ShehiaName.Contains(term)).
                    Select(p => new { p.districtId,p.ShehiaName }).ToListAsync();
                return Ok(names);
               
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
