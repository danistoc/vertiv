using PasswordManagementApi.Contracts;
using PasswordManagementApi.Infrastructure.DataAccess;
using PasswordManagementApi.Infrastructure.PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManagementApi.Infrastructure
{
    public class PasswordManagementService
    {
        private readonly IRepository repository;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly int passwordTitmeToLiveSeconds;

        public PasswordManagementService(IRepository repository, IPasswordGenerator passwordGenerator, int passwordTitmeToLiveSeconds)
        {
            this.repository = repository;
            this.passwordGenerator = passwordGenerator;
            this.passwordTitmeToLiveSeconds = passwordTitmeToLiveSeconds;
        }

        public GeneratePasswordResponse GenerateUserPassword(GeneratePasswordRequest request)
        {
            var user = repository.GetUser(request.UserId);
            if (user == null)
            {
                throw new Exception($"User with id:{request.UserId} not found.");
            }

            var password = passwordGenerator.Generate();
            var passwordCreationDate = request.CreationDate.ToDateTime();
            var passwordExpirationDate = passwordCreationDate.AddSeconds(passwordTitmeToLiveSeconds);
            
            repository.UpdatePassword(request.UserId, password, passwordCreationDate, passwordExpirationDate);

            return new GeneratePasswordResponse
            {
                UserId = request.UserId,
                Password = password,
                ExpirationDate = passwordExpirationDate.ToFormattedString()
            };
        }

        public List<UserDto> ListUsers()
        {
            var users = repository.GetAllUsers();

            return users?.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                PasswordExpirationDate = u.PasswordExpirationDate?.ToFormattedString()
            }).ToList();
        }
    }
}
