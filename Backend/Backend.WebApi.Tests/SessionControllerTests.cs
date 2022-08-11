using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.WebApi.Controllers;
using Backend.BusinessLogic.Interface;
using Moq;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Tools;

namespace Backend.WebApi.Tests
{
    [TestClass]
    public class SessionControllerTests
    {
        [TestMethod]
        public void LoginTest()
        {
            var guid = Guid.NewGuid();
            var user = new User()
            {
                Email = "email@email.com",
                Password = "pass"
            };
            var login = new LoginDT
            {
                Email = "email@email.com",
                Password = "pass"
            };

            var mock = new Mock<IUserSession>(MockBehavior.Strict);
            mock.Setup(m => m.Login(login.Email, login.Password)).Returns(guid);

            var mockULogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockULogic.Setup(m => m.GetByEmail(login.Email)).Returns(user);

            var controller = new SessionsController(mock.Object, mockULogic.Object);

            var result = controller.Login(login);
            var createdResult = result as CreatedResult;
            var statusCode = createdResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(201, statusCode);
        }

        [TestMethod]
        public void LogOutTest()
        {
            var auth = Guid.NewGuid();

            var mock = new Mock<IUserSession>(MockBehavior.Strict);
            mock.Setup(m => m.Logout(auth));

            var mockUL = new Mock<IUserLogic>(MockBehavior.Strict);

            var controller = new SessionsController(mock.Object, mockUL.Object);

            var result = controller.Logout(auth);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as string;

            mock.VerifyAll();
            Assert.AreEqual("Successfully logged out of session " + auth.ToString(), value);
        }

    }
}