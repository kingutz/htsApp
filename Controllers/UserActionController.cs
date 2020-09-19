using htsApp.Data;
using htsApp.Models;
using htsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace htsApp.Controllers
{
    [Authorize(Roles = "admin,analyst")]
    public class UserActionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContext> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;
        public UserActionController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
          UserManager<ApplicationUser> userManager, ILogger<ApplicationDbContext> logger,
          ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.HTS.ToListAsync());
        }
        public IActionResult Create()
        {
            var cout = new List<UserMgt>();

            var h = _context.HTS.GroupBy(i => i.CreatedByUser).Select(g => new
            {
                username = g.Key,
                edit = g.Sum(i => i.Edited ? 1 : 0),
                create = g.Count()
            }).OrderByDescending(p => p.create);


            //var a1 = _context.Pmt.Select(i => new { username = i.CreatedByUser,
            //  edit=i.Edited.Equals(true).ToString() , create = i.CreatedByUser.Count()});

            foreach (var ct in h)
            {
                cout.Add(new UserMgt
                {
                    UserName = ct.username,
                    Created = ct.create,
                    Edited = ct.edit

                });
            }

            return View(cout);
        }
    }
}
