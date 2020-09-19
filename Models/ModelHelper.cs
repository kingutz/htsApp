using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp.Models
{
    public class ModelHelper
    {
        public static List<object> MultiLineData()
        {
            List<object> objs = new List<object>();
            objs.Add(new[] { "x", "sin(x)", "cos(x)", "sin(x)^2" });
            for (int i = 0; i < 70; i++)
            {
                double x = 0.1 * i;
                objs.Add(new[] { x, Math.Sin(x), Math.Cos(x), Math.Sin(x) * Math.Sin(x) });
            }
            return objs;
        }
    }
}
