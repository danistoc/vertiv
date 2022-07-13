using PasswordManagementApi.Infrastructure.DataAccess.InMemory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManagementApi.Infrastructure.DataAccess.InMemory
{
    public class InMemoryRepository : IRepository
    {
        private int LastUserId = 0;
        private static Dictionary<int, User> Users = new Dictionary<int, User>();

        public int AddUser(string username)
        {
            var uid = ++LastUserId;
            Users.Add(uid, new User
            {
                Id = uid,
                Username = username,
            });
            return uid;
        }

        public User GetUser(int userId)
        {
            var user = Users.ContainsKey(userId) ? Users[userId] : null;
            return user;
        }

        public List<User> GetAllUsers()
        {
            return Users.Select(p => p.Value).ToList();
        }

        public void UpdatePassword(int userId, string password, DateTime creationDate, DateTime expirationDate)
        {
            var user = Users[userId];

            user.Password = password;
            user.PasswordCreationDate = creationDate;
            user.PasswordExpirationDate = expirationDate;
        }
    }
}
