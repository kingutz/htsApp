using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class Facility : EntityData
    {
       // public int Id { get; set; }
        [Display(Name = "Facility name")]
        public string FacilityName { get; set; }
        public string Description { get; set; }
    }
}
