using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Services.Contract
{
    public interface IUserService
    {
        // Method to get user
        Task<User> GetUser(string email, string key);

        //Method to save user
        Task<User> SaveUser(User model);
    }
}
