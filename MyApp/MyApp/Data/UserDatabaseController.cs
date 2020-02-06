using MyApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyApp.Data
{
    public class UserDatabaseController
    {
        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<User>();
        }

        public List<User> GetAllUsers()
        {
            return database.Query<User>("SELECT * FROM User");
        }

        public User GetUser(int id)
        {
            return database.Table<User>().FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return database.Table<User>().FirstOrDefault(u => u.Username == username);
        }
        public User GetUserByEmail(string email)
        {
            return database.Table<User>().FirstOrDefault(u => u.Email == email);
        }

        public void InsertUser(User user)
        {
            database.Insert(user);
        }

        public void DeleteUser(int id)
        {
            database.Delete<User>(id);
        }

        public void UpdateUser(string change, int id)
        {
            database.Query<User>("UPDATE User SET " + change + " WHERE Id = " + id);
        }
    }
}
