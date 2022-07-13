using PasswordManagementApi.Infrastructure.DataAccess.InMemory.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManagementApi.Infrastructure.DataAccess
{
    public interface IRepository
    {
        int AddUser(string username);
        User GetUser(int userId);
        List<User> GetAllUsers();
        void UpdatePassword(int userId, string password, DateTime creationDate, DateTime expirationDate);
    }
}
