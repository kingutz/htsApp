using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class District : EntityData
    {
        //public int Id { get; set; }
        [Display(Name = "District name")]
        public string DistrictName { get; set; }
        public string Description { get; set; }


        public ICollection<Shehia> Shehiadata { get; set; }
    }
}
