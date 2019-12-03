using Microsoft.AspNetCore.Identity;

namespace WebStore.Model.Entity.Identity
{
    public class Role : IdentityRole {
        public enum Roles
        {
            Administrator,
            User
        }
    }
}
