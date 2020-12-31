
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Models
{
    public class Product 
    {
        [Display(Name = "Product name")]
        public string name { get; set; }

        public DateTime production { get; set; }

        public int companyId { get; set; }

        public int expiresInDays { get; set; }

        public DateTime ExpiryDate() => production.AddDays(expiresInDays);
    }
}
