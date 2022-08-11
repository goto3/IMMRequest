using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.Repository.Interface;
using Moq;
using System.Collections.Generic;
using System;
using Backend.Tools;
using Backend.BusinessLogic.Interface;

namespace Backend.BusinessLogic.Tests
{
    [TestClass]
    public class UserTest
    {
        private IUserLogic userLogic;
        private Mock<IRepository<User>> mockURepository;

        [TestInitialize]
        public void Setup()
        {
            mockURepository = new Mock<IRepository<User>>(MockBehavior.Strict);

            userLogic = new UserLogic(mockURepository.Object);
        }

        [TestMethod]
        public void CreateUserTest1()
        {
            var user = new User
            {
                Name = "Nombre1",
                Email = "Email1@email.com",
                Password = "Pass1"
            };

            mockURepository.Setup(m => m.GetAll()).Returns(() => new List<User>());
            mockURepository.Setup(m => m.Add(It.IsAny<User>()));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Create(user);

            mockURepository.VerifyAll();
            Assert.AreEqual(user.Name, result.Name);
        }

        [TestMethod]
        public void GetUserTest1()
        {
            var guid = Guid.NewGuid();
            var user = new User() { Id = guid };

            mockURepository.Setup(m => m.Get(guid)).Returns(user);

            var result = userLogic.Get(guid);

            mockURepository.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetUserByEmailTest1()
        {
            var guid = Guid.NewGuid();
            var user1 = new User() { Id = guid, Email = "user1@email.com" };
            var user2 = new User() { Id = Guid.NewGuid(), Email = "user2@email.com" };
            var list = new List<User>() { user1, user2 };
            mockURepository.Setup(m => m.GetAll()).Returns(list);

            var result = userLogic.GetByEmail("user1@email.com");

            mockURepository.VerifyAll();
            Assert.AreEqual(user1, result);
        }

        [TestMethod]
        public void GetAllUsersTest1()
        {
            var list = new List<User>();
            mockURepository.Setup(m => m.GetAll()).Returns(list);

            var result = userLogic.GetAll();

            mockURepository.VerifyAll();
            Assert.AreEqual(list, result);
        }

        [TestMethod]
        public void RemoveUserTest1()
        {
            var user = new User();
            var guid = Guid.NewGuid();

            mockURepository.Setup(m => m.Get(guid)).Returns(user);
            mockURepository.Setup(m => m.Remove(user));
            mockURepository.Setup(m => m.Save());

            userLogic.Remove(guid);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateUserTest1()
        {
            var u1 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Email = "email@domain.com",
                Name = "Username",
                Password = "Password"
            };
            var u2 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Email = "NewEmail@domain.com",
                Name = "NewUsername",
                Password = "NewPassword"
            };

            mockURepository.Setup(m => m.Get(u2.Id)).Returns(u1);
            mockURepository.Setup(m => m.GetAll()).Returns(new List<User>());
            mockURepository.Setup(m => m.Update(u1));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Update(u2);

            mockURepository.VerifyAll();
            Assert.AreEqual(result, u1);
            Assert.AreEqual(result.Id, u2.Id);
            Assert.AreEqual(result.Email, u2.Email);
            Assert.AreEqual(result.Name, u2.Name);
            Assert.AreEqual(result.Password, u2.Password);
        }

        [TestMethod]
        public void UpdateUserTest2()
        {
            var u1 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Email = "email@domain.com",
                Name = "Username",
                Password = "Password"
            };
            var u2 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Email = "email@domain.com",
                Name = "NewUsername",
                Password = ""
            };

            mockURepository.Setup(m => m.Get(u2.Id)).Returns(u1);
            mockURepository.Setup(m => m.GetAll()).Returns(new List<User>() { u1 });
            mockURepository.Setup(m => m.Update(u1));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Update(u2);

            mockURepository.VerifyAll();
            Assert.AreEqual(result, u1);
            Assert.AreEqual(result.Id, u2.Id);
            Assert.AreEqual(result.Email, u2.Email);
            Assert.AreEqual(result.Name, u2.Name);
            Assert.AreEqual(result.Password, u2.Password);
        }

        [TestMethod]
        public void UpdateUserTest3()
        {
            var u1 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Email = "email1@domain.com",
                Name = "Username1",
                Password = "Password"
            };
            var u2 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Email = "email2@domain.com",
                Name = "Username2",
                Password = "NewPassword"
            };
            var u3 = new User()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Email = "email2@domain.com",
                Name = "NewUsername2",
                Password = "NewPassword"
            };

            mockURepository.Setup(m => m.Get(u2.Id)).Returns(u2);
            mockURepository.Setup(m => m.GetAll()).Returns(new List<User>() { u1, u2 });
            mockURepository.Setup(m => m.Update(u2));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Update(u3);

            mockURepository.VerifyAll();
            Assert.AreEqual(result, u2);
            Assert.AreEqual(result.Id, u2.Id);
            Assert.AreEqual(result.Email, u2.Email);
            Assert.AreEqual(result.Name, u2.Name);
            Assert.AreEqual(result.Password, u2.Password);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_NOT_FOUND")]
        public void UpdateInvalidUserTest1()
        {
            var guid = Guid.NewGuid();
            var user = new User() { Id = guid };

            mockURepository.Setup(m => m.Get(guid)).Returns(() => null);

            userLogic.Update(user);

            mockURepository.VerifyAll();
        }


        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_NOT_FOUND")]
        public void RemoveInvalidUserTest1()
        {
            var user = new User();
            var guid = Guid.NewGuid();

            mockURepository.Setup(m => m.Get(guid)).Returns(() => null);

            userLogic.Remove(guid);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_NULL")]
        public void CreateInvalidUserTest1()
        {
            userLogic.Create(null);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_EMAIL_NULL_EMPTY")]
        public void CreateInvalidUserTest2()
        {
            var user = new User
            {
                Name = "Nombre1",
                Email = "",
                Password = "Pass1"
            };
            mockURepository.Setup(m => m.GetAll()).Returns(() => new List<User>());
            mockURepository.Setup(m => m.Add(It.IsAny<User>()));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Create(user);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_EMAIL_INVALID")]
        public void CreateInvalidUserTest3()
        {
            var user = new User
            {
                Name = "Nombre1",
                Email = "not a valid email address",
                Password = "Pass1"
            };
            mockURepository.Setup(m => m.GetAll()).Returns(() => new List<User>());
            mockURepository.Setup(m => m.Add(It.IsAny<User>()));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Create(user);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_PASSWORD_NULL_EMPTY")]
        public void CreateInvalidUserTest4()
        {
            var user = new User
            {
                Name = "Nombre1",
                Email = "email@domain.com",
                Password = ""
            };
            mockURepository.Setup(m => m.GetAll()).Returns(() => new List<User>());
            mockURepository.Setup(m => m.Add(It.IsAny<User>()));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Create(user);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_DUPLICATE")]
        public void CreateExistUserTest()
        {
            var user = new User
            {
                Name = "Nombre1",
                Email = "Email@email.com",
                Password = "Pass"
            };
            mockURepository.Setup(m => m.GetAll()).Returns(() => new List<User>() { new User() { Email = "Email@email.com" } });
            mockURepository.Setup(m => m.Add(It.IsAny<User>()));
            mockURepository.Setup(m => m.Save());

            var result = userLogic.Create(user);

            mockURepository.VerifyAll();
        }

        [TestMethod]
        public void GetValidUserTest()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Pepe",
                Password = "Pass"
            };
            mockURepository.Setup(m => m.Get(user.Id)).Returns(user);

            var result = userLogic.Get(user.Id);

            mockURepository.VerifyAll();
            Assert.AreEqual(user.Name, result.Name);
        }
    }
}

