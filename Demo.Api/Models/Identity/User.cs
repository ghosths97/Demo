using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Models.Identity
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        public User(string userName) : base(userName)
        {

        }

        public string Name { get; set; }
    }
}
