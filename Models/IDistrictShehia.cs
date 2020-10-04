using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
   public interface IDistrictShehia
    {
        IEnumerable<District> GetDistrict();
        IEnumerable<Shehia> GetShehia(long Id);
    }
}
