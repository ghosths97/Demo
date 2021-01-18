using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Models.Domain
{
    public class CompanyResponse
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }
    }
}
