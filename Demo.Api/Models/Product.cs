using Demo.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string name { get; set; }

        [DateValidator(ErrorMessage = "Production date should not be in future")]
        public DateTime production { get; set; }

        public int CompanyId { get; set; }

        public int ExpiresInDays { get; set; }

        public DateTime ExpiryDate() => production.AddDays(ExpiresInDays);
    }
}
