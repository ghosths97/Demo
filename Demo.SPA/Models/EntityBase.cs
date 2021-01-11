using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Models
{
    public class EntityBase
    {
        public int id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
