using System;

namespace PasswordManagementApi.Infrastructure.PasswordGenerator
{
    public class GuidPasswordGenerator : IPasswordGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
