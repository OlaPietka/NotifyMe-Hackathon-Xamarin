using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Service
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        bool IsUsernameExist(string username);
        bool IsEmailExist(string email);
        void DeleteUser(int id);
        void UpdateUser(string change, int id);
        void InsertUser(User user);
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
    }
}
