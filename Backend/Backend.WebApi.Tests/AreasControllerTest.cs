using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.WebApi.Controllers;
using Backend.BusinessLogic.Interface;
using Moq;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Backend.Tools;

namespace Backend.WebApi.Tests
{
    [TestClass]
    public class AreaControllerTest
    {
        [TestMethod]
        public void GetAllAreasTest1()
        {
            var listTopics = new List<Topic>() { new Topic() };
            var area1 = new Area() { Id = Guid.NewGuid(), Name = "area1", Topics = listTopics };
            var list = new List<Area>() { area1 };

            var ALogicMock = new Mock<ILogic<Area>>(MockBehavior.Strict);

            ALogicMock.Setup(m => m.GetAll()).Returns(list);

            var controller = new AreasController(ALogicMock.Object);

            var result = controller.GetAllAreas();
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var model = okResult.Value as List<AreaDT>;

            ALogicMock.VerifyAll();
            Assert.AreEqual(list.Count, model.Count);
            Assert.IsTrue(model.Exists(a => a.Id == area1.Id && a.Name == area1.Name));
        }

        [TestMethod]
        public void GetAreaTest1()
        {
            var guid = Guid.NewGuid();
            var area1 = new Area() { Id = guid, Name = "area1" };

            var ALogicMock = new Mock<ILogic<Area>>(MockBehavior.Strict);
            ALogicMock.Setup(m => m.Get(guid)).Returns(area1);

            var controller = new AreasController(ALogicMock.Object);

            var result = controller.Get(guid);
            var okResult = result as ObjectResult;
            var value = okResult.Value as AreaDT;

            ALogicMock.VerifyAll();
            Assert.AreEqual(area1.Id, value.Id);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_AREA_NOT_FOUND")]
        public void GetAreaTest2()
        {
            var guid = Guid.NewGuid();

            var ALogicMock = new Mock<ILogic<Area>>(MockBehavior.Strict);
            ALogicMock.Setup(m => m.Get(guid)).Returns(() => null);

            var controller = new AreasController(ALogicMock.Object);

            var result = controller.Get(guid);

            ALogicMock.VerifyAll();
        }



    }
}