using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class Role
    {
        public string Id { get; set; }

        [Display(Name = "Role Name:")]
        public string Name { get; set; }
        [Display(Name = "Select Role:")]
        public bool Selected { get; set; }
    }
}
