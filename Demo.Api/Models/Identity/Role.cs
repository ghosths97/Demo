using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Models.Identity
{
    public class Role : IdentityRole
    {
        public Role()
        {

        }

        public Role(string roleName) : base(roleName)
        {

        }
    }
}
