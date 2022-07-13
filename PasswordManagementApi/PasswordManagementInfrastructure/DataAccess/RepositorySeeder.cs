using PasswordManagementApi.Infrastructure.PasswordGenerator;
using System;

namespace PasswordManagementApi.Infrastructure.DataAccess
{
    public class RepositorySeeder
    {
        private readonly IRepository repository;
        private readonly IPasswordGenerator pwdGenerator;

        public RepositorySeeder(IRepository repository, IPasswordGenerator pwdGenerator)
        {
            this.repository = repository;
            this.pwdGenerator = pwdGenerator;
        }

        public void Seed()
        {
            AddExpiredPasswords();
            AddValidPassword();
            AddEmptyUsers();
        }

        private void AddExpiredPasswords()
        {
            var uid = repository.AddUser("John_Smith");
            var pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddSeconds(30));

            uid = repository.AddUser("Foo_Bar");
            pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(-1).AddSeconds(30));
        }

        private void AddEmptyUsers()
        {
            repository.AddUser("Lola");
            repository.AddUser("Someone_007");
        }

        private void AddValidPassword()
        {
            int uid = repository.AddUser("John_Snow");
            var pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now, DateTime.Now.AddHours(1));
        }
    }
}
