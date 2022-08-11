using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.Tools;
using Backend.WebApi.Controllers;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Backend.WebApi.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void CreateValidUserTest()
        {
            var user = new UserDT
            {
                Name = "Pepe",
                Password = "Pass"
            };
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<User>())).Returns(user.ToEntity());
            var controller = new UsersController(mock.Object);

            var result = controller.Post(user);
            var createdResult = result as CreatedResult;
            var statusCode = createdResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual("User created successfully", createdResult.Location);
            Assert.AreEqual(201, statusCode);
        }

        [TestMethod]
        public void GetValidUserTest()
        {
            var guid = Guid.NewGuid();
            var user = new UserDT
            {
                Name = "Pepe",
                Password = "Pass"
            };
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(user.ToEntity());
            var controller = new UsersController(mock.Object);

            var result = controller.Get("", guid);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var model = okResult.Value as UserDT;

            mock.VerifyAll();
            Assert.AreEqual(user.Email, model.Email);
            Assert.AreEqual(user.Name, model.Name);
            Assert.AreEqual(user.Password, model.Password);
        }

        [TestMethod]
        public void PutTest1()
        {
            var guid = Guid.NewGuid();
            var user = new UserDT
            {
                Name = "Pepe",
                Email = "email@email.com",
                Password = "Pass"
            };

            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<User>())).Returns(user.ToEntity());
            var controller = new UsersController(mock.Object);

            var result = controller.Put("", guid, user);
            var okResult = result as OkObjectResult;
            var statusCode = okResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void DeleteTest1()
        {
            var guid = Guid.NewGuid();

            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Remove(guid));
            var controller = new UsersController(mock.Object);

            var result = controller.Delete("", guid);
            var okResult = result as OkObjectResult;
            var statusCode = okResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USER_DUPLICATE")]
        public void CreateExistUserTest()
        {
            var user = new UserDT
            {
                Name = "Pepe",
                Password = "Pass"
            };
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<User>())).Throws(new BackendException("ERR_USER_DUPLICATE"));
            var controller = new UsersController(mock.Object);

            var result = controller.Post(user);

            mock.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_USERS_GET_INCORRECT")]
        public void GetInexistentUserTest1()
        {
            var guid = Guid.NewGuid();

            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(() => null);
            var controller = new UsersController(mock.Object);

            var result = controller.Get("", guid);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllUsersTest1()
        {
            var ULogicMock = new Mock<IUserLogic>(MockBehavior.Strict);

            ULogicMock.Setup(m => m.GetAll()).Returns(new List<User>());

            var controller = new UsersController(ULogicMock.Object);

            var result = controller.GetAll("");
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<UserDT>;

            ULogicMock.VerifyAll();
        }

    }
}