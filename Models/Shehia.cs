using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class Shehia : EntityData
    {
        //public int Id { get; set; }
        public Int64 districtId { get; set; }
        public string ShehiaName { get; set; }
        public string Description { get; set; }

        public District district { get; set; }
    }
}
