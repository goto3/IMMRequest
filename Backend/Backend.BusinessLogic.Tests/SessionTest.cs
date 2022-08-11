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
    public class SessionTest
    {
        private IUserSession sessionLogic;
        private Mock<IRepository<UserSession>> mockUSRepository;
        private Mock<IUserLogic> mockURepository;

        [TestInitialize]
        public void Setup()
        {
            mockUSRepository = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            mockURepository = new Mock<IUserLogic>(MockBehavior.Strict);

            sessionLogic = new SessionLogic(mockUSRepository.Object, mockURepository.Object);
        }

        [TestMethod]
        public void IsLoggedTest1()
        {
            var guid = Guid.NewGuid();
            var sessionList = new List<UserSession>();
            mockUSRepository.Setup(m => m.GetAll()).Returns(sessionList);

            var result = sessionLogic.IsLogued(guid);

            mockUSRepository.VerifyAll();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLoggedTest2()
        {
            var guid = Guid.NewGuid();
            var us1 = new UserSession() { Token = guid };
            var sessionList = new List<UserSession>() { us1 };

            mockUSRepository.Setup(m => m.GetAll()).Returns(sessionList);

            var result = sessionLogic.IsLogued(guid);

            mockUSRepository.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginTest1()
        {
            var email = "email@domain.com";
            var password = "password";
            var u1 = new User() { Email = email, Password = password };
            var userList = new List<User>() { u1 };

            mockUSRepository.Setup(m => m.Add(It.IsAny<UserSession>()));
            mockUSRepository.Setup(m => m.Save());

            mockURepository.Setup(m => m.GetAll()).Returns(userList);

            var result = sessionLogic.Login(email, password);

            mockUSRepository.VerifyAll();
            mockURepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_USER_NOT_FOUND")]
        public void LoginTest2()
        {
            var email = "email@domain.com";
            var password = "PASS1";
            var u1 = new User() { Email = email, Password = "PASS2" };
            var userList = new List<User>() { u1 };

            mockUSRepository.Setup(m => m.Add(It.IsAny<UserSession>()));
            mockUSRepository.Setup(m => m.Save());

            mockURepository.Setup(m => m.GetAll()).Returns(userList);

            var result = sessionLogic.Login(email, password);

            mockUSRepository.VerifyAll();
            mockURepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_USER_NOT_FOUND")]
        public void LoginTest3()
        {
            var email = "email@domain.com";
            var password = "password";
            var u1 = new User() { Email = "sdsd", Password = password };
            var userList = new List<User>() { u1 };

            mockUSRepository.Setup(m => m.Add(It.IsAny<UserSession>()));
            mockUSRepository.Setup(m => m.Save());

            mockURepository.Setup(m => m.GetAll()).Returns(userList);

            var result = sessionLogic.Login(email, password);

            mockUSRepository.VerifyAll();
            mockURepository.VerifyAll();
        }

        [TestMethod]
        public void LogOutTest1()
        {
            var guid = Guid.NewGuid();
            var session = new UserSession() { Token = guid };

            mockUSRepository.Setup(m => m.Get(guid)).Returns(session);
            mockUSRepository.Setup(m => m.Remove(session));
            mockUSRepository.Setup(m => m.Save());

            sessionLogic.Logout(guid);

            mockUSRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_SESSION_TOKEN_NULL_NOT_FOUND")]
        public void LogoutErrorTest1()
        {
            var guid = Guid.NewGuid();

            mockUSRepository.Setup(m => m.Get(guid)).Returns(() => null);

            sessionLogic.Logout(guid);

            mockUSRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_EMAIL")]
        public void LoginInvalidTest1()
        {
            var result = sessionLogic.Login(null, "Password");
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_EMAIL")]
        public void LoginInvalidTest2()
        {
            var result = sessionLogic.Login("", "Password");
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_PASSWORD")]
        public void LoginInvalidTest3()
        {
            var result = sessionLogic.Login("email", null);
        }

        [TestMethod]
        [ExpectedException(typeof(BackendException), "ERR_LOGIN_PASSWORD")]
        public void LoginInvalidTest4()
        {
            var result = sessionLogic.Login("email", "");
        }


    }
}