using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using htsApp.Data;
using htsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace htsApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public AdminController(ILogger<HomeController> logger, ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager
                )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }
       
        public ViewResult Index() => View(_roleManager.Roles);

        public async Task<IActionResult> UserIndex()
        {
            var user = new List<UserData>();

            try
            {
                var users = await _userManager.Users.ToListAsync();
                foreach (var item in users)
                {
                    user.Add(new UserData()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UserName = item.UserName,
                        Email = item.Email,
                        MobileNo=item.PhoneNumber
                        
                    });
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.GetBaseException().Message);
            }

            return View(user);
        }
        public ViewResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string name)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var roleexist = await _roleManager.RoleExistsAsync(name);
                    if (!roleexist)
                    {
                        var result = await _roleManager.CreateAsync(new IdentityRole(name));
                    }

                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save a record. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator,This role exist in database.");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);


            if (role.Id == null)
            {
                return NotFound();
            }

            return View(role);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                //IdentityResult result = await _roleManager.DeleteAsync(role);
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", _roleManager.Roles);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            List<ApplicationUser> Members = new List<ApplicationUser>();
            List<ApplicationUser> NotMembers = new List<ApplicationUser>();

            foreach (var userapp in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(userapp, role.Name)
                ? Members : NotMembers;
                list.Add(userapp);
            }
            return View(new RoleEdit
            {
                RoleName = role,
                MemberToRole = Members,
                NotMembersToRole = NotMembers
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserToRole model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdToAdd ?? new string[] { })
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                foreach (string userId in model.IdToDelete ?? new string[] { })
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user,
                        model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
