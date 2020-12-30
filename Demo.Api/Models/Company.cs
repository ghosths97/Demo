using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Company : EntityBase
    {
        [StringLength(10)]
        public string name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
