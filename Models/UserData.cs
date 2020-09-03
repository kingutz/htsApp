using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class UserData
    {
        public string Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Display(Name = "Full name")]
        public string Name { get; set; }
        [Display(Name = "E mail")]
        public string Email { get; set; }
        [Display(Name = "Phone number")]
        public string MobileNo { get; set; }
        public Role[] roles { get; set; }
    }
}
