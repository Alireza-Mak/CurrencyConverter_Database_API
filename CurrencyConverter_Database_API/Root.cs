using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter_Database_API
{
    public class Root
    {
        public string license { get; set; }
        public Rate rates { get; set; }
        public long timestamp { get; set; }
    }

    public class Rate
    {
        public double USD { get; set; }
        public double CAD { get; set; }
        public double EUR { get; set; }
        public double GBP { get; set; }
        public double QAR { get; set; }
    }
}
