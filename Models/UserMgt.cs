using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class UserMgt
    {
        [Display(Name = "Edited Record")]
        public int Edited { get; set; }
        [Display(Name = "Created Record")]
        public int Created { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
