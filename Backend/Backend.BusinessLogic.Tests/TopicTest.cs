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
    public class TopicTest
    {
        private ILogic<Topic> topicLogic;
        private Mock<IRepository<Topic>> mockTRepository;

        [TestInitialize]
        public void Setup()
        {
            mockTRepository = new Mock<IRepository<Topic>>(MockBehavior.Strict);

            topicLogic = new TopicLogic(mockTRepository.Object);
        }

        [TestMethod]
        public void CreateValidTopicTest1()
        {
            var a = new Area();
            var tt = new TopicType() { Name = "Nombre1" };
            var topic = new Topic() { Name = "Nombre1", Area = a, TopicTypes = new List<TopicType>() { tt } };
            tt.Topic = topic;

            mockTRepository.Setup(m => m.Add(topic));
            mockTRepository.Setup(m => m.Save());

            var result = topicLogic.Create(topic);

            mockTRepository.VerifyAll();
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        public void CreateExistentTopicTest1()
        {
            var topic = new Topic() { Name = "Nombre1" };
            var a = new Area() { Topics = new List<Topic>() { topic } };
            topic.Area = a;

            var result = topicLogic.Create(topic);

            mockTRepository.VerifyAll();
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        public void GetTopicTest1()
        {
            var guid = Guid.NewGuid();
            var topic = new Topic() { Id = guid };

            mockTRepository.Setup(m => m.Get(guid)).Returns(topic);

            var result = topicLogic.Get(guid);

            mockTRepository.VerifyAll();
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        public void GetAllTopicTest1()
        {
            var topicList = new List<Topic>();

            mockTRepository.Setup(m => m.GetAll()).Returns(topicList);

            var result = topicLogic.GetAll();

            mockTRepository.VerifyAll();
            Assert.AreEqual(topicList, result);
        }

        [TestMethod]
        public void RemoveTopicTest1()
        {
            var guid = Guid.NewGuid();
            var topic = new Topic() { Id = guid };

            mockTRepository.Setup(m => m.Get(guid)).Returns(topic);
            mockTRepository.Setup(m => m.Remove(topic));
            mockTRepository.Setup(m => m.Save());

            topicLogic.Remove(guid);

            mockTRepository.VerifyAll();
        }

    }
}