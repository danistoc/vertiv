using System;

namespace PasswordManagementApi.Infrastructure.PasswordGenerator
{
    public class NumericPassworgGenerator : IPasswordGenerator
    {
        public string Generate()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
    }
}
