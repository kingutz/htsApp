using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htsApp.Data;
using htsApp.Models;
using htsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace htsApp.Controllers
{
    [Route("api/htscsv")]
    [ApiController]
    [Authorize(Roles = "admin,analyst,dataentry")]
    public class HtsCSController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;

        public HtsCSController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));


        }

        [HttpGet]
        public async Task<ActionResult> CSV()

        {
            bool isSuperUser = User.IsInRole("admin") || User.IsInRole("analyst");
            List<HTSData> hTs = new List<HTSData>();
            var builder = new StringBuilder();

            if (isSuperUser)
            {
                hTs = await _context.HTS.ToListAsync();

                builder.AppendLine("ID,NameofSite,OutreachName,District,Month,Year,OutreachOrganizer," +
            "Ownership,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus,ResidenceShehia," +
            "ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling,Reasonfortesting," +
            "Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued,Nooffemalecondomsissued," +
            "Referredto,CTCNumber,RemarksName");

                foreach (var item in hTs)
                {
                    builder.AppendLine($"{item.ID},{item.NameofSite},{item.OutreachName}," +
                    $"{item.District},{item.Month},{item.Year}," +
                    $"{item.OutreachOrganizer},{item.Ownership}," +
                    $"{item.ClientCode},{item.TypeofAttendance}," +
                    $"{item.Age},{item.Sex},{item.MaritalStatus},{item.ResidenceShehia}," +
                    $"{item.ResidenceDistrict},{item.Occupation},{item.ClientCode},{item.Disabled}," +
                    $"{item.HTCApproach},{item.TypeofCounselling},{item.Reasonfortesting},{item.Result}," +
                    $"{item.DiscordantCouple},{item.ResultPositiveType},{item.Receivedresult},{item.Noofmalecondomsissued}," +
                    $"{item.Nooffemalecondomsissued},{item.Referredto},{item.CTCNumber},{item.RemarksName}");
                }
                return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "hts.csv");
            }

            else

                hTs = await _context.HTS.Where(p => p.CreatedByUser == currentUserService.GetCurrentUsername()).ToListAsync();

            builder.AppendLine("ID,NameofSite,OutreachName,District,Month,Year,OutreachOrganizer," +
            "Ownership,DateData,ClientCode,TypeofAttendance,Age,Sex,MaritalStatus,ResidenceShehia," +
            "ResidenceDistrict,Occupation,ClientsCategory,Disabled,HTCApproach,TypeofCounselling,Reasonfortesting," +
            "Result,DiscordantCouple,ResultPositiveType,Receivedresult,Noofmalecondomsissued,Nooffemalecondomsissued," +
            "Referredto,CTCNumber,RemarksName");

            foreach (var item in hTs)
            {
                builder.AppendLine($"{item.ID},{item.NameofSite},{item.OutreachName}," +
                       $"{item.District},{item.Month},{item.Year}," +
                       $"{item.OutreachOrganizer},{item.Ownership},{item.DateData}," +
                       $"{item.ClientCode},{item.TypeofAttendance}," +
                       $"{item.Age},{item.Sex},{item.MaritalStatus},{item.ResidenceShehia}," +
                       $"{item.ResidenceDistrict},{item.Occupation},{item.ClientCode},{item.Disabled}," +
                       $"{item.HTCApproach},{item.TypeofCounselling},{item.Reasonfortesting},{item.Result}," +
                       $"{item.DiscordantCouple},{item.ResultPositiveType},{item.Receivedresult},{item.Noofmalecondomsissued}," +
                       $"{item.Nooffemalecondomsissued},{item.Referredto},{item.CTCNumber},{item.RemarksName}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "hts.csv");

        }

    }
}
