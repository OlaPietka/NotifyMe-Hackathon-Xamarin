using MyApp.Data;
using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Service
{
    public class UserRepository : IUserRepository
    {
        UserDatabaseController database;

        public UserRepository()
        {
            database = new UserDatabaseController();
        }

        public List<User> GetAllUsers()
        {
            return database.GetAllUsers();
        }

        public bool IsUsernameExist(string username)
        {
            foreach (User u in GetAllUsers())
            {
                if (u.Username.Equals(username))
                    return true;
            }

            return false;
        }

        public bool IsEmailExist(string email)
        {
            foreach (User u in GetAllUsers())
            {
                if (u.Email.Equals(email))
                    return true;
            }

            return false;
        }

        public void DeleteUser(int id)
        {
            database.DeleteUser(id);
        }

        public void UpdateUser(string change, int id)
        {
            database.UpdateUser(change, id);
        }

        public void InsertUser(User user)
        {
            database.InsertUser(user);
        }

        public User GetUserById(int id)
        {
            return database.GetUser(id);
        }

        public User GetUserByUsername(string username)
        {
            return database.GetUserByUsername(username);
        }

        public User GetUserByEmail(string email)
        {
            return database.GetUserByEmail(email);
        }
    }
}
