using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Company : AuditableEntity
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
