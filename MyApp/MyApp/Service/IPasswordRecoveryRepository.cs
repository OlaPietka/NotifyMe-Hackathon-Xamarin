using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Service
{
    public interface IPasswordRecoveryRepository
    {
        List<PasswordRecovery> GetAll();
        PasswordRecovery GetByEmail(string email);
        void DeleteEmail(int id);
        void InsertEmail(PasswordRecovery entity);
        bool IsEmailExist(string email);
    }
}
