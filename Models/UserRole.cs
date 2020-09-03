using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class UserRole
    {
        //public string RoleId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
