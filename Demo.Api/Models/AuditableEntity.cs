using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class AuditableEntity
    {
        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModificationOn { get; set; }
    }
}
