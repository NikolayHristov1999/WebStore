namespace WebStore.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Data.Models;

    public interface IUsersService
    {
        Task AddUserToRole(UserManager<ApplicationUser> user, string role);
    }
}
