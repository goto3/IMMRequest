using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.Repository.Interface;
using Moq;
using System.Collections.Generic;
using System;
using Backend.BusinessLogic.Interface;
using Backend.Tools;

namespace Backend.BusinessLogic.Tests
{
    [TestClass]
    public class TopicTypeTest
    {
        private ITopicTypeLogic topicTypeLogic;
        private Mock<IRepository<TopicType>> mockTTRepository;
        private Mock<ILogic<Topic>> mockTLogic;
        private Mock<ILogic<AdditionalField>> mockAFLogic;

        [TestInitialize]
        public void Setup()
        {
            mockTTRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockTLogic = new Mock<ILogic<Topic>>(MockBehavior.Strict);
            mockAFLogic = new Mock<ILogic<AdditionalField>>(MockBehavior.Strict);

            topicTypeLogic = new TopicTypeLogic(mockTTRepository.Object, mockTLogic.Object, mockAFLogic.Object);
        }

        [TestMethod]
        public void CreateValidTopicTypeTest1()
        {
            var tt = new TopicType()
            {
                Name = "Nombre1",
                Topic = new Topic() { Id = Guid.NewGuid() },
            };

            mockTTRepository.Setup(m => m.Add(It.IsAny<TopicType>()));
            mockTTRepository.Setup(m => m.Save());
            mockTLogic.Setup(m => m.Get(tt.Topic.Id)).Returns(new Topic());

            var result = topicTypeLogic.Create(tt);

            mockTTRepository.VerifyAll();
            mockTLogic.VerifyAll();
            Assert.AreEqual(tt, result);
        }

        [TestMethod]
        public void CreateValidTopicTypeTest2()
        {
            var af1 = new AdditionalField() { Name = "Field name", FieldType = "Text" };
            var tt = new TopicType()
            {
                Name = "Nombre1",
                Topic = new Topic() { Id = Guid.NewGuid() },
                AdditionalFields = new List<AdditionalField>() { af1 }
            };
            mockTTRepository.Setup(m => m.Add(It.IsAny<TopicType>()));
            mockTTRepository.Setup(m => m.Save());
            mockTLogic.Setup(m => m.Get(tt.Topic.Id)).Returns(new Topic());
            mockAFLogic.Setup(m => m.Validate(It.IsAny<AdditionalField>()));

            var result = topicTypeLogic.Create(tt);

            mockTTRepository.VerifyAll();
            mockTLogic.VerifyAll();
            Assert.AreEqual(tt, result);
        }

        [TestMethod]
        public void CreateValidTopicTypeTest3()
        {
            var topic = new Topic() { Id = Guid.NewGuid(), TopicTypes = new List<TopicType>() };
            var tt1 = new TopicType() { Name = "Nombre1", Topic = topic };
            topic.TopicTypes.Add(tt1);
            var tt2 = new TopicType() { Name = "Nombre2", Topic = topic };

            mockTTRepository.Setup(m => m.Add(It.IsAny<TopicType>()));
            mockTTRepository.Setup(m => m.Save());
            mockTLogic.Setup(m => m.Get(tt2.Topic.Id)).Returns(topic);

            var result = topicTypeLogic.Create(tt2);

            mockTTRepository.VerifyAll();
            mockTLogic.VerifyAll();
            Assert.AreEqual(tt2, result);
        }

        [TestMethod]
        public void GetTopicTypeTest1()
        {
            var guid = Guid.NewGuid();
            var tt = new TopicType() { Id = guid };

            mockTTRepository.Setup(m => m.Get(guid)).Returns(tt);

            var result = topicTypeLogic.Get(guid);

            mockTTRepository.VerifyAll();
            Assert.AreEqual(tt, result);
        }

        [TestMethod]
        public void GetAllTopicTypeTest1()
        {
            var ttList = new List<TopicType>();

            mockTTRepository.Setup(m => m.GetAll()).Returns(ttList);

            var result = topicTypeLogic.GetAll();

            mockTTRepository.VerifyAll();
            Assert.AreEqual(ttList, result);
        }

        [TestMethod]
        public void RemoveTopicTypeTest1()
        {
            var guid = Guid.NewGuid();
            var tt = new TopicType() { Id = guid };

            mockTTRepository.Setup(m => m.Get(guid)).Returns(tt);
            mockTTRepository.Setup(m => m.Remove(tt));
            mockTTRepository.Setup(m => m.Save());

            topicTypeLogic.Remove(guid);

            mockTTRepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_NOT_FOUND")]
        public void RemoveTopicTypeTest2()
        {
            var guid = Guid.NewGuid();

            mockTTRepository.Setup(m => m.Get(guid)).Returns(() => null);

            topicTypeLogic.Remove(guid);

            mockTTRepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_TOPIC_NULL_NOTFOUND")]
        public void CreateInValidTopicTypeTest1()
        {
            var tt = new TopicType() { Name = "Nombre1" };
            var result = topicTypeLogic.Create(tt);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_TOPIC_NULL_NOTFOUND")]
        public void CreateInValidTopicTypeTest2()
        {
            var tt = new TopicType() { Name = "Nombre1", Topic = new Topic() { Id = Guid.NewGuid() } };

            mockTLogic.Setup(m => m.Get(tt.Topic.Id)).Returns(() => null);

            var result = topicTypeLogic.Create(tt);

            mockTLogic.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_NAME_NULL_ERROR")]
        public void CreateInValidTopicTypeTest3()
        {
            var tt = new TopicType() { Topic = new Topic() { Id = Guid.NewGuid() } };
            var result = topicTypeLogic.Create(tt);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_NAME_DUPLICATE")]
        public void CreateInValidTopicTypeTest4()
        {
            List<TopicType> listTT = new List<TopicType>();
            var topic = new Topic() { Id = Guid.NewGuid(), Name = "Topic Name", TopicTypes = listTT };
            var tt1 = new TopicType() { Name = "TopicType name", Topic = topic };
            listTT.Add(tt1);
            var tt2 = new TopicType() { Name = "TopicType name", Topic = topic };

            mockTLogic.Setup(m => m.Get(tt2.Topic.Id)).Returns(topic);

            var result = topicTypeLogic.Create(tt2);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_ADDITONALFIELD_NAME_DUPLICATE")]
        public void CreateInValidTopicTypeTest5()
        {
            var af1 = new AdditionalField() { Name = "Field name", FieldType = "Text" };
            var af2 = new AdditionalField() { Name = "Field name", FieldType = "Text" };
            var tt = new TopicType()
            {
                Name = "Nombre1",
                Topic = new Topic() { Id = Guid.NewGuid() },
                AdditionalFields = new List<AdditionalField>() { af1, af2 }
            };

            mockTLogic.Setup(m => m.Get(tt.Topic.Id)).Returns(new Topic());
            mockAFLogic.Setup(m => m.Validate(It.IsAny<AdditionalField>()));

            var result = topicTypeLogic.Create(tt);
        }

        [TestMethod]
        public void GetReportBTest1()
        {
            var dateNow = DateTime.Now.AddMinutes(-1);
            var listRequests1 = new List<Request>() { new Request() { Date = DateTime.Now }, new Request() { Date = DateTime.Now } };
            var listRequests2 = new List<Request>() { new Request() { Date = dateNow }, new Request() { Date = dateNow }, new Request() { Date = dateNow } };
            var tt1 = new TopicType() { Name = "TT1", Created = dateNow.AddSeconds(6), Requests = listRequests1 };
            var tt2 = new TopicType() { Name = "TT2", Created = dateNow, Requests = listRequests1 };
            var tt3 = new TopicType() { Name = "TT3", Created = dateNow.AddSeconds(5), Requests = listRequests1 };
            var tt4 = new TopicType() { Name = "TT4", Created = dateNow.AddSeconds(30), Requests = listRequests2 };
            var listTT = new List<TopicType>() { tt1, tt2, tt3, tt4 };

            mockTTRepository.Setup(m => m.GetAll()).Returns(listTT);

            var result = topicTypeLogic.GetReportB(dateNow.AddSeconds(1), DateTime.Now);

            mockTTRepository.VerifyAll();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(0, result.FindIndex(tt => tt.Name == "TT2"));
            Assert.AreEqual(1, result.FindIndex(tt2 => tt2.Name == "TT3"));
            Assert.AreEqual(2, result.FindIndex(tt2 => tt2.Name == "TT1"));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_TOPICTYPE_REPORTB_DATES_INVALID")]
        public void GetReportBInvalidTest1()
        {
            var result = topicTypeLogic.GetReportB(DateTime.Now.AddSeconds(1), DateTime.Now);
        }

    }
}