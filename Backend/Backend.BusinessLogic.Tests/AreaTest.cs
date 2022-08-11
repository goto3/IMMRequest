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
    public class AreaTest
    {
        private ILogic<Area> areaLogic;
        private Mock<IRepository<Area>> mockARepository;

        [TestInitialize]
        public void Setup()
        {
            mockARepository = new Mock<IRepository<Area>>(MockBehavior.Strict);

            areaLogic = new AreaLogic(mockARepository.Object);
        }

        [TestMethod]
        public void CreateValidAreaTest1()
        {
            var area = new Area() { Name = "Nombre1" };

            mockARepository.Setup(m => m.Add(area));
            mockARepository.Setup(m => m.GetAll()).Returns(new List<Area>());
            mockARepository.Setup(m => m.Save());

            var result = areaLogic.Create(area);

            mockARepository.VerifyAll();
            Assert.AreEqual(area, result);
        }

        [TestMethod]
        public void CreateExistentValidAreaTest1()
        {
            var area = new Area() { Name = "Nombre1" };

            mockARepository.Setup(m => m.GetAll()).Returns(new List<Area>() { area });

            var result = areaLogic.Create(area);

            mockARepository.VerifyAll();
            Assert.AreEqual(area, result);
        }

        [TestMethod]
        public void GetAreaTest1()
        {
            var guid = Guid.NewGuid();
            var area = new Area() { Id = guid };

            mockARepository.Setup(m => m.Get(guid)).Returns(area);

            var result = areaLogic.Get(guid);

            mockARepository.VerifyAll();
            Assert.AreEqual(area, result);
        }

        [TestMethod]
        public void GetAllAreaTest1()
        {
            var areaList = new List<Area>();

            mockARepository.Setup(m => m.GetAll()).Returns(areaList);

            var result = areaLogic.GetAll();

            mockARepository.VerifyAll();
            Assert.AreEqual(areaList, result);
        }

        [TestMethod]
        public void RemoveAreaTest1()
        {
            var guid = Guid.NewGuid();
            var area = new Area() { Id = guid };

            mockARepository.Setup(m => m.Get(guid)).Returns(area);
            mockARepository.Setup(m => m.Remove(area));
            mockARepository.Setup(m => m.Save());

            areaLogic.Remove(guid);

            mockARepository.VerifyAll();
        }

    }
}