using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entity.Identity;

namespace WebStore.Interfaces.Api
{
    public interface IUsersClient :
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserClaimStore<User>,
        IUserTwoFactorStore<User>,
        IUserLockoutStore<User>,
        IUserLoginStore<User>
    {
    }
}
