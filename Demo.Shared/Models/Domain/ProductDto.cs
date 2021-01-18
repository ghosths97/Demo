using Demo.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Models.Domain
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [DateValidator("Production Date must be in past")]
        public DateTime production { get; set; }

        [Required]     
        [Range(1, int.MaxValue, ErrorMessage = "Please select company")]
        public int CompanyId { get; set; }

        [Required]
        public int ExpiresInDays { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpiryDate() => production.AddDays(ExpiresInDays); 
    }
}
