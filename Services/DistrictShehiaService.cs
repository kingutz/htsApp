using htsApp.Data;
using htsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Services
{
    public class DistrictShehiaService : IDistrictShehia
    {
        
        
       
        private readonly ApplicationDbContext _context;
      
        public DistrictShehiaService(ApplicationDbContext context)

        {
            _context = context;
          
           
        }
        //public async Task<IEnumerable<District>> GetDistrictAsync()
        //{
        //    return await _context.district.ToListAsync();
        //}

        public IEnumerable<Shehia> GetShehia(long Id)
        {
            return _context.shehia.Where(d=>d.districtId==Id).ToList();
        }

        IEnumerable<District> IDistrictShehia.GetDistrict()
        {
            return  _context.district.ToList();
        }
    }
}
