using MyApp.Data;
using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Service
{
    public class PasswordRecoveryRepository : IPasswordRecoveryRepository
    {
        PasswordRecoveryDatabaseController database;

        public PasswordRecoveryRepository()
        {
            database = new PasswordRecoveryDatabaseController();
        }

        public List<PasswordRecovery> GetAll()
        {
            return database.GetAll();
        }

        public PasswordRecovery GetByEmail(string email)
        {
            return database.GetByEmail(email);
        }

        public void DeleteEmail(int id)
        {
            database.Delete(id);
        }

        public void InsertEmail(PasswordRecovery entity)
        {
            database.Insert(entity);
        }
        public bool IsEmailExist(string email)
        {
            foreach (PasswordRecovery p in database.GetAll())
            {
                if (p.Email == email)
                    return true;
            }

            return false;
        }
    }
}
