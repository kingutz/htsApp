using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class Shehia : EntityData
    {
        //public int Id { get; set; }
        [Display(Name = "District")]
        public Int64 districtId { get; set; }
        [Display(Name = "Shehia name")]
        public string ShehiaName { get; set; }
        public string Description { get; set; }

        public District district { get; set; }
    }
}
