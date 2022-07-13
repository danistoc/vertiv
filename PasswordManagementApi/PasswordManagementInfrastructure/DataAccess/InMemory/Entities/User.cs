using System;

namespace PasswordManagementApi.Infrastructure.DataAccess.InMemory.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordCreationDate { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
    }
}
