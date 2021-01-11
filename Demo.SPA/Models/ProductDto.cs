using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Models
{
    public class ProductDto : EntityBase
    {
        [Required]
        [StringLength(10)]
        public string name { get; set; }

        public DateTime production { get; set; }

        [Required]
        public int companyId { get; set; }

        public int expiresInDays { get; set; }

        public DateTime ExpiryDate() => production.AddDays(expiresInDays);
    }
}
