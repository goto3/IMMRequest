using Backend.BusinessLogic;
using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.Tools;
using IMMRequestDataImport;
using IMMRequestDataImport.domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Backend.DataImport.Tests
{
    [TestClass]
    public class DataImportLogicTests
    {
        private DataImportLogic importLogic;
        private Mock<ILogic<Area>> areaLogic;
        private Mock<ILogic<Topic>> topicLogic;
        private Mock<ITopicTypeLogic> topicTypeLogic;

        [TestInitialize]
        public void Setup()
        {
            areaLogic = new Mock<ILogic<Area>>(MockBehavior.Strict);
            topicLogic = new Mock<ILogic<Topic>>(MockBehavior.Strict);
            topicTypeLogic = new Mock<ITopicTypeLogic>(MockBehavior.Strict);

            importLogic = new DataImportLogic(areaLogic.Object, topicLogic.Object, topicTypeLogic.Object);
        }

        [TestMethod]
        public void ImportTest1()
        {
            var areas = 0;
            var topics = 0;
            var topicTypes = 0;
            areaLogic.Setup(m => m.Create(It.IsAny<Area>())).Returns(new Area()).Callback(() => areas++);
            topicLogic.Setup(m => m.Create(It.IsAny<Topic>())).Returns(new Topic()).Callback(() => topics++);
            topicTypeLogic.Setup(m => m.Create(It.IsAny<TopicType>())).Returns(new TopicType()).Callback(() => topicTypes++);

            var field1 = new ImportField() { Data = "test1.json" };
            var listFields = new List<ImportField>() { field1 };

            var listNewData = importLogic.Import(@"JSONImport.dll", listFields);

            areaLogic.VerifyAll();
            topicLogic.VerifyAll();
            topicTypeLogic.VerifyAll();

            Assert.AreEqual(2, listNewData.Count);
            Assert.AreEqual(3, topics);
            Assert.AreEqual(4, topicTypes);
        }

        [TestMethod]
        public void ImportTest2()
        {
            var areas = 0;
            var topics = 0;
            var topicTypes = 0;
            areaLogic.Setup(m => m.Create(It.IsAny<Area>())).Returns(new Area()).Callback(() => areas++);
            topicLogic.Setup(m => m.Create(It.IsAny<Topic>())).Returns(new Topic()).Callback(() => topics++);
            topicTypeLogic.Setup(m => m.Create(It.IsAny<TopicType>())).Returns(new TopicType()).Callback(() => topicTypes++);

            var field1 = new ImportField() { Data = "test1.xml" };
            var listFields = new List<ImportField>() { field1 };

            var listNewData = importLogic.Import(@"XMLImport.dll", listFields);

            areaLogic.VerifyAll();
            topicLogic.VerifyAll();
            topicTypeLogic.VerifyAll();

            Assert.AreEqual(2, listNewData.Count);
            Assert.AreEqual(4, topics);
            Assert.AreEqual(4, topicTypes);
        }

        [TestMethod]
        public void GetAllDllTest1()
        {
            var listNewData = importLogic.GetAll();

            Assert.AreEqual(2, listNewData.Count);
        }
    }
}
