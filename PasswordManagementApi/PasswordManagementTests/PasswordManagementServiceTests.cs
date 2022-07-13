using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManagementApi.Contracts;
using PasswordManagementApi.Infrastructure;
using PasswordManagementApi.Infrastructure.DataAccess;
using PasswordManagementApi.Infrastructure.DataAccess.InMemory.Entities;
using PasswordManagementApi.Infrastructure.PasswordGenerator;
using System;

namespace PasswordManagementTests
{
    [TestClass]
    public class PasswordManagementServiceTests
    {
        private int passwordTTLInSeconds = 30;
        private IPasswordGenerator pwdGenerator;
        private IRepository repository;
        private PasswordManagementService service;

        [TestInitialize]
        public void Init()
        {
            pwdGenerator = A.Fake<IPasswordGenerator>();
            repository = A.Fake<IRepository>();
            service = new PasswordManagementService(repository, pwdGenerator, passwordTTLInSeconds);
        }

        [TestMethod]
        public void GeneratePassword_ShouldThrowError_WhenUserIsNotFound()
        {
            A.CallTo(() => repository.GetUser(A<int>.Ignored)).Returns(null);

            var generateRq = new GeneratePasswordRequest
            {
                UserId = 1,
                CreationDate = DateTime.Now.ToFormattedString()
            };

            Assert.ThrowsException<Exception>(() => { service.GenerateUserPassword(generateRq); });
        }

        [TestMethod]
        public void GeneratePassword_ShouldCreateNewPassword_AndSetPasswordExpirationAfterTheGivenTimeInSeconds()
        {
            var existingUser = SetupExpectedUser();
            var newPassword = SetupPasswordGenerated();

            var generateRq = new GeneratePasswordRequest
            {
                UserId = existingUser.Id,
                CreationDate = DateTime.Now.ToFormattedString()
            };

            var response = service.GenerateUserPassword(generateRq);

            var creationDate = generateRq.CreationDate.ToDateTime();
            var expectedExpirationDate = creationDate.AddSeconds(passwordTTLInSeconds);

            A.CallTo(() => repository.UpdatePassword(existingUser.Id, newPassword, creationDate, expectedExpirationDate)).MustHaveHappened(1, Times.Exactly);

            Assert.AreEqual(existingUser.Id, response.UserId);
            Assert.AreEqual(newPassword, response.Password);
            Assert.AreEqual(expectedExpirationDate.ToFormattedString(), response.ExpirationDate);
        }

        private string SetupPasswordGenerated()
        {
            var newPassword = "password";
            A.CallTo(() => pwdGenerator.Generate()).Returns(newPassword);
            return newPassword;
        }

        private User SetupExpectedUser()
        {
            var existingUser = new User { Id = 1 };
            A.CallTo(() => repository.GetUser(A<int>.That.Matches(x => x == existingUser.Id))).Returns(existingUser);
            return existingUser;
        }
    }
}
