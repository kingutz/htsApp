using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Services
{
   public interface ICurrentUserService
    {
      public  string GetCurrentUsername();
    }
}
