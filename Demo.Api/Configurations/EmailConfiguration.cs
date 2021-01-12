using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Configurations
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public bool Test { get; set; }
        public int Retry { get; set; }
    }

}
