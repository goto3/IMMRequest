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
    public class TopictypeControllerTest
    {
        [TestMethod]
        public void CreateValidTopicTypeTest1()
        {
            var pv1 = new List<string>() { "text1", "text2" };
            var AdditionalField1 = new AdditionalFieldDT() { Name = "topic1", FieldType = "Text", PossibleValues = pv1 };

            var listAF = new List<AdditionalFieldDT>() { AdditionalField1 };
            var topicTypeModel = new TopicTypeDT { Name = "TT name", AdditionalFields = listAF };

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.Create(It.IsAny<TopicType>())).Returns(topicTypeModel.ToEntity());

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.Post("", topicTypeModel);
            var createdResult = result as CreatedResult;
            var statusCode = createdResult.StatusCode;

            TTLogicMock.VerifyAll();
            Assert.AreEqual(statusCode, 201);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Topic null or not found.")]
        public void CreateInValidTopicTypeTest1()
        {
            var listAF = new List<AdditionalFieldDT>();
            var topicTypeModel = new TopicTypeDT { Name = "TT name", AdditionalFields = listAF };

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.Create(It.IsAny<TopicType>())).Throws(new ArgumentException("Topic null or not found."));

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.Post("", topicTypeModel);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var model = okResult.Value as string;

            TTLogicMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteTest1()
        {
            var guid = Guid.NewGuid();

            var mock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Remove(guid));
            var controller = new TopicTypesController(mock.Object);

            var result = controller.Delete("", guid);
            var okResult = result as OkObjectResult;
            var statusCode = okResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetTopicTypeTest1()
        {
            var guid = Guid.NewGuid();
            var topicType1 = new TopicType() { Id = guid, Name = "topicType1" };

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.Get(guid)).Returns(topicType1);

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.Get(guid);
            var okResult = result as ObjectResult;
            var value = okResult.Value as TopicTypeDT;

            TTLogicMock.VerifyAll();
            Assert.AreEqual(topicType1.Id, value.Id);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_NOT_FOUND")]
        public void GetTopicTypeTest2()
        {
            var guid = Guid.NewGuid();

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.Get(guid)).Returns(() => null);

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.Get(guid);

            TTLogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllTopicTypeTest1()
        {
            var tt1 = new TopicType();
            var topicTypeList = new List<TopicType>() { tt1 };

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.GetAll()).Returns(topicTypeList);

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.GetAll("");
            var okResult = result as ObjectResult;
            var value = okResult.Value as List<TopicTypeDT>;

            TTLogicMock.VerifyAll();
            Assert.AreEqual(1, value.Count);
        }

        [TestMethod]
        public void GetReportBTest1()
        {
            var req = new Request();
            var listTT = new List<TopicType>() { new TopicType() { Name = "ttname1", Requests = new List<Request>() { req } } };

            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            TTLogicMock.Setup(m => m.GetReportB(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(listTT);

            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.GetReportB("", DateTime.Now.AddSeconds(-10).ToString(), DateTime.Now.ToString());
            var okResult = result as ObjectResult;
            var value = okResult.Value as List<TopicTypeReportBDT>;

            TTLogicMock.VerifyAll();
            Assert.AreEqual(1, value.Count);
            Assert.IsTrue(value.Find(tt => tt.Name == "ttname1") != null);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_DATE_FORMAT")]
        public void GetReportBInvalidTest1()
        {
            var TTLogicMock = new Mock<ITopicTypeLogic>(MockBehavior.Strict);
            var controller = new TopicTypesController(TTLogicMock.Object);

            var result = controller.GetReportB("", "not a valid date", DateTime.Now.ToString());
        }

    }
}