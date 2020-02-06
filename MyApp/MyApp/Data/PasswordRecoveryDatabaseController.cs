using MyApp.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyApp.Data
{
    public class PasswordRecoveryDatabaseController
    {
        SQLiteConnection database;

        public PasswordRecoveryDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<PasswordRecovery>();
        }

        public List<PasswordRecovery> GetAll()
        {
            return database.Query<PasswordRecovery>("SELECT * FROM PasswordRecovery");
        }


        public PasswordRecovery GetByEmail(string email)
        {
            return database.Table<PasswordRecovery>().FirstOrDefault(u => u.Email == email);
        }

        public void Insert(PasswordRecovery pass)
        {
            database.Insert(pass);
        }

        public void Delete(int id)
        {
            database.Delete<PasswordRecovery>(id);
        }
    }
}
