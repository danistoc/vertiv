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
        }

        private void AddExpiredPasswords()
        {
            var uid = repository.AddUser("JohnSmith");
            var pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddSeconds(30));

            uid = repository.AddUser("FooBar");
            pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(-1).AddSeconds(30));
        }

        private void AddValidPassword()
        {
            int uid = repository.AddUser("JohnSnow");
            var pwd = pwdGenerator.Generate();
            repository.UpdatePassword(uid, pwd, DateTime.Now, DateTime.Now.AddHours(1));
        }
    }
}
