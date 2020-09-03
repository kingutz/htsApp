using htsApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class RoleEdit
    {
        public IdentityRole RoleName { get; set; }
        public IEnumerable<ApplicationUser> MemberToRole { get; set; }
        public IEnumerable<ApplicationUser> NotMembersToRole { get; set; }
    }
}
