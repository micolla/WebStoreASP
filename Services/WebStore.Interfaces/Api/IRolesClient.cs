using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entity.Identity;

namespace WebStore.Interfaces.Api
{
    public interface IRolesClient : IRoleStore<Role> { }
}
