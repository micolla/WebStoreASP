using Microsoft.AspNetCore.Identity;

namespace WebStore.Model.Entity.Identity
{
    public class Role : IdentityRole {
        public const string Administrator = "Administrator";
        public const string User = "User";
    }
}
