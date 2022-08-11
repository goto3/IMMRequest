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
    public class TopicControllerTest
    {

        [TestMethod]
        public void GetTopicTest1()
        {
            var listTT = new List<TopicType>() { new TopicType() };
            var guid = Guid.NewGuid();
            var topic1 = new Topic() { Id = guid, Name = "topic1", TopicTypes = listTT };

            var TLogicMock = new Mock<ILogic<Topic>>(MockBehavior.Strict);
            TLogicMock.Setup(m => m.Get(guid)).Returns(topic1);

            var controller = new TopicsController(TLogicMock.Object);

            var result = controller.Get(guid);
            var okResult = result as ObjectResult;
            var value = okResult.Value as TopicDT;

            TLogicMock.VerifyAll();
            Assert.AreEqual(topic1.Id, value.Id);
            Assert.AreEqual(topic1.Name, value.Name);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPIC_NOT_FOUND")]
        public void GetTopicTest2()
        {
            var guid = Guid.NewGuid();

            var TLogicMock = new Mock<ILogic<Topic>>(MockBehavior.Strict);
            TLogicMock.Setup(m => m.Get(guid)).Returns(() => null);

            var controller = new TopicsController(TLogicMock.Object);

            var result = controller.Get(guid);

            TLogicMock.VerifyAll();
        }

    }
}