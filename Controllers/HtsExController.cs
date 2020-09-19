using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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
    [Route("api/htsex")]
    [ApiController]
    [Authorize(Roles = "admin,analyst,dataentry")]
    public class HtsExController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService currentUserService;

        public HtsExController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }



        [HttpGet]
        public async Task<ActionResult> Exdoc()

        {
            bool isSuperUser = User.IsInRole("admin") || User.IsInRole("analyst");
            List<HTSData> hTs = new List<HTSData>();

            if (isSuperUser)
            {
                hTs = await _context.HTS.ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("hTs");
                    var currentRow = 1;
                   
                    worksheet.Cell(currentRow, 1).Value = "ID";
                    worksheet.Cell(currentRow, 2).Value = "NameofSite";
                    worksheet.Cell(currentRow, 3).Value = "OutreachName";
                    worksheet.Cell(currentRow, 4).Value = "District";
                    worksheet.Cell(currentRow, 5).Value = "Month";
                    worksheet.Cell(currentRow, 6).Value = "Year";
                    worksheet.Cell(currentRow, 7).Value = "OutreachOrganizer";
                    worksheet.Cell(currentRow, 8).Value = "Ownership";
                    worksheet.Cell(currentRow, 9).Value = "DateData";
                    worksheet.Cell(currentRow, 10).Value = "ClientCode";
                    worksheet.Cell(currentRow, 11).Value = "TypeofAttendance";
                    worksheet.Cell(currentRow, 12).Value = "Age";
                    worksheet.Cell(currentRow, 13).Value = "Sex";
                    worksheet.Cell(currentRow, 14).Value = "MaritalStatus";
                    worksheet.Cell(currentRow, 15).Value = "ResidenceShehia";
                    worksheet.Cell(currentRow, 16).Value = "ResidenceDistrict";
                    worksheet.Cell(currentRow, 17).Value = "Occupation";
                    worksheet.Cell(currentRow, 18).Value = "ClientsCategory";
                    worksheet.Cell(currentRow, 19).Value = "Disabled";
                    worksheet.Cell(currentRow, 20).Value = "HTCApproach";
                    worksheet.Cell(currentRow, 21).Value = "TypeofCounselling";
                    worksheet.Cell(currentRow, 22).Value = "Reasonfortesting";
                    worksheet.Cell(currentRow, 23).Value = "Result";
                    worksheet.Cell(currentRow, 24).Value = "DiscordantCouple";
                    worksheet.Cell(currentRow, 25).Value = "ResultPositiveType";
                    worksheet.Cell(currentRow, 26).Value = "Receivedresult";
                    worksheet.Cell(currentRow, 27).Value = "Noofmalecondomsissued";
                    worksheet.Cell(currentRow, 28).Value = "Nooffemalecondomsissued";
                    worksheet.Cell(currentRow, 29).Value = "Referredto";
                    worksheet.Cell(currentRow, 30).Value = "CTCNumber";
                    worksheet.Cell(currentRow, 31).Value = "RemarksName";
                                      
                    foreach (var item in hTs)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.ID;
                        worksheet.Cell(currentRow, 2).Value = item.NameofSite;
                        worksheet.Cell(currentRow, 3).Value = item.OutreachName;
                        worksheet.Cell(currentRow, 4).Value = item.District;
                        worksheet.Cell(currentRow, 5).Value = item.Month;
                        worksheet.Cell(currentRow, 6).Value = item.Year;
                        worksheet.Cell(currentRow, 7).Value = item.OutreachOrganizer;
                        worksheet.Cell(currentRow, 8).Value = item.Ownership;
                        worksheet.Cell(currentRow, 9).Value = item.DateData;
                        worksheet.Cell(currentRow, 10).Value = item.ClientCode;
                        worksheet.Cell(currentRow, 11).Value = item.TypeofAttendance;
                        worksheet.Cell(currentRow, 12).Value = item.Age;
                        worksheet.Cell(currentRow, 13).Value = item.Sex;
                        worksheet.Cell(currentRow, 14).Value = item.MaritalStatus;
                        worksheet.Cell(currentRow, 15).Value = item.ResidenceShehia;
                        worksheet.Cell(currentRow, 16).Value = item.ResidenceDistrict;
                        worksheet.Cell(currentRow, 17).Value = item.Occupation;
                        worksheet.Cell(currentRow, 18).Value = item.ClientsCategory;
                        worksheet.Cell(currentRow, 19).Value = item.Disabled;
                        worksheet.Cell(currentRow, 20).Value = item.HTCApproach;
                        worksheet.Cell(currentRow, 21).Value = item.TypeofCounselling;
                        worksheet.Cell(currentRow, 22).Value = item.Reasonfortesting;
                        worksheet.Cell(currentRow, 23).Value = item.Result;
                        worksheet.Cell(currentRow, 24).Value = item.DiscordantCouple;
                        worksheet.Cell(currentRow, 25).Value = item.ResultPositiveType;
                        worksheet.Cell(currentRow, 26).Value = item.Receivedresult;
                        worksheet.Cell(currentRow, 27).Value = item.Noofmalecondomsissued;
                        worksheet.Cell(currentRow, 28).Value = item.Nooffemalecondomsissued;
                        worksheet.Cell(currentRow, 29).Value = item.Referredto;
                        worksheet.Cell(currentRow, 30).Value = item.CTCNumber;
                        worksheet.Cell(currentRow, 31).Value = item.RemarksName;
                        

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxlformats-officedocument.spreadsheetml.sheet"
                            , "Htsnfo.xlsx");
                    }
                }
            }
            else
            {
                hTs = await _context.HTS.Where(p => p.CreatedByUser == currentUserService.GetCurrentUsername()).ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("hTs");
                    var currentRow = 1;


                    worksheet.Cell(currentRow, 1).Value = "ID";
                    worksheet.Cell(currentRow, 2).Value = "NameofSite";
                    worksheet.Cell(currentRow, 3).Value = "OutreachName";
                    worksheet.Cell(currentRow, 4).Value = "District";
                    worksheet.Cell(currentRow, 5).Value = "Month";
                    worksheet.Cell(currentRow, 6).Value = "Year";
                    worksheet.Cell(currentRow, 7).Value = "OutreachOrganizer";
                    worksheet.Cell(currentRow, 8).Value = "Ownership";
                    worksheet.Cell(currentRow, 9).Value = "DateData";
                    worksheet.Cell(currentRow, 10).Value = "ClientCode";
                    worksheet.Cell(currentRow, 11).Value = "TypeofAttendance";
                    worksheet.Cell(currentRow, 12).Value = "Age";
                    worksheet.Cell(currentRow, 13).Value = "Sex";
                    worksheet.Cell(currentRow, 14).Value = "MaritalStatus";
                    worksheet.Cell(currentRow, 15).Value = "ResidenceShehia";
                    worksheet.Cell(currentRow, 16).Value = "ResidenceDistrict";
                    worksheet.Cell(currentRow, 17).Value = "Occupation";
                    worksheet.Cell(currentRow, 18).Value = "ClientsCategory";
                    worksheet.Cell(currentRow, 19).Value = "Disabled";
                    worksheet.Cell(currentRow, 20).Value = "HTCApproach";
                    worksheet.Cell(currentRow, 21).Value = "TypeofCounselling";
                    worksheet.Cell(currentRow, 22).Value = "Reasonfortesting";
                    worksheet.Cell(currentRow, 23).Value = "Result";
                    worksheet.Cell(currentRow, 24).Value = "DiscordantCouple";
                    worksheet.Cell(currentRow, 25).Value = "ResultPositiveType";
                    worksheet.Cell(currentRow, 26).Value = "Receivedresult";
                    worksheet.Cell(currentRow, 27).Value = "Noofmalecondomsissued";
                    worksheet.Cell(currentRow, 28).Value = "Nooffemalecondomsissued";
                    worksheet.Cell(currentRow, 29).Value = "Referredto";
                    worksheet.Cell(currentRow, 30).Value = "CTCNumber";
                    worksheet.Cell(currentRow, 31).Value = "RemarksName";

                    foreach (var item in hTs)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.ID;
                        worksheet.Cell(currentRow, 2).Value = item.NameofSite;
                        worksheet.Cell(currentRow, 3).Value = item.OutreachName;
                        worksheet.Cell(currentRow, 4).Value = item.District;
                        worksheet.Cell(currentRow, 5).Value = item.Month;
                        worksheet.Cell(currentRow, 6).Value = item.Year;
                        worksheet.Cell(currentRow, 7).Value = item.OutreachOrganizer;
                        worksheet.Cell(currentRow, 8).Value = item.Ownership;
                        worksheet.Cell(currentRow, 9).Value = item.DateData;
                        worksheet.Cell(currentRow, 10).Value = item.ClientCode;
                        worksheet.Cell(currentRow, 11).Value = item.TypeofAttendance;
                        worksheet.Cell(currentRow, 12).Value = item.Age;
                        worksheet.Cell(currentRow, 13).Value = item.Sex;
                        worksheet.Cell(currentRow, 14).Value = item.MaritalStatus;
                        worksheet.Cell(currentRow, 15).Value = item.ResidenceShehia;
                        worksheet.Cell(currentRow, 16).Value = item.ResidenceDistrict;
                        worksheet.Cell(currentRow, 17).Value = item.Occupation;
                        worksheet.Cell(currentRow, 18).Value = item.ClientsCategory;
                        worksheet.Cell(currentRow, 19).Value = item.Disabled;
                        worksheet.Cell(currentRow, 20).Value = item.HTCApproach;
                        worksheet.Cell(currentRow, 21).Value = item.TypeofCounselling;
                        worksheet.Cell(currentRow, 22).Value = item.Reasonfortesting;
                        worksheet.Cell(currentRow, 23).Value = item.Result;
                        worksheet.Cell(currentRow, 24).Value = item.DiscordantCouple;
                        worksheet.Cell(currentRow, 25).Value = item.ResultPositiveType;
                        worksheet.Cell(currentRow, 26).Value = item.Receivedresult;
                        worksheet.Cell(currentRow, 27).Value = item.Noofmalecondomsissued;
                        worksheet.Cell(currentRow, 28).Value = item.Nooffemalecondomsissued;
                        worksheet.Cell(currentRow, 29).Value = item.Referredto;
                        worksheet.Cell(currentRow, 30).Value = item.CTCNumber;
                        worksheet.Cell(currentRow, 31).Value = item.RemarksName;

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxlformats-officedocument.spreadsheetml.sheet"
                            , "HtsInfo.xlsx");
                    }
                }


            }


        }









    }
}
