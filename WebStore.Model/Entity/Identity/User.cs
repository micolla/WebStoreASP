using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Model.Entity.Identity
{
    public class User : IdentityUser 
    {
        public const string AdminUserName="Administrator";
        public const string AdminDefaultPassword = "AdminDefaultP$wd123";
    }
}
