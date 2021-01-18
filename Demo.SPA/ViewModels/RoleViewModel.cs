using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.ViewModels
{
    public class RoleViewModel
    {
        public string Name { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
        public string Id { get; set; }

    }

    public class Permission
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
    }

}
