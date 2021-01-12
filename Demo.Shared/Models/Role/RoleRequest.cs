using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Models.Role
{
    public class RoleRequest
    {
        public string RoleId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string[] Permissions { get; set; }
        
    }
}
