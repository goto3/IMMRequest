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
    public class AdditionalFieldTest
    {
        private ILogic<AdditionalField> additionalFieldLogic;
        private Mock<IRepository<AdditionalField>> mockAFRepository;

        [TestInitialize]
        public void Setup()
        {
            mockAFRepository = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);

            additionalFieldLogic = new AdditionalFieldLogic(mockAFRepository.Object);
        }

        [TestMethod]
        public void CreateValidAdditionalFieldTest1()
        {
            var af1 = new AdditionalField() { Name = "Name", FieldType = "Text" };

            mockAFRepository.Setup(m => m.Add(It.IsAny<AdditionalField>()));
            mockAFRepository.Setup(m => m.Save());

            var result = additionalFieldLogic.Create(af1);

            mockAFRepository.VerifyAll();
            Assert.AreEqual(af1, result);
        }

        [TestMethod]
        public void CreateValidAdditionalFieldTest2()
        {
            string[] pv1 = { "1", "10" };
            var af1 = new AdditionalField() { Name = "Name", FieldType = "Integer", PossibleValues = pv1 };

            mockAFRepository.Setup(m => m.Add(It.IsAny<AdditionalField>()));
            mockAFRepository.Setup(m => m.Save());

            var result = additionalFieldLogic.Create(af1);

            mockAFRepository.VerifyAll();
            Assert.AreEqual(af1, result);
        }

        [TestMethod]
        public void CreateValidAdditionalFieldTest3()
        {
            var af1 = new AdditionalField() { Name = "Name", FieldType = "Bool" };

            mockAFRepository.Setup(m => m.Add(It.IsAny<AdditionalField>()));
            mockAFRepository.Setup(m => m.Save());

            var result = additionalFieldLogic.Create(af1);

            mockAFRepository.VerifyAll();
            Assert.AreEqual(af1, result);
        }

        [TestMethod]
        public void GetAdditionalFieldTest1()
        {
            var guid = Guid.NewGuid();
            var af1 = new AdditionalField() { Id = guid };

            mockAFRepository.Setup(m => m.Get(guid)).Returns(af1);

            var result = additionalFieldLogic.Get(guid);

            mockAFRepository.VerifyAll();
            Assert.AreEqual(af1, result);
        }

        [TestMethod]
        public void GetAllAdditionalFieldTest1()
        {
            var afList = new List<AdditionalField>() { };

            mockAFRepository.Setup(m => m.GetAll()).Returns(afList);

            var result = additionalFieldLogic.GetAll();

            mockAFRepository.VerifyAll();
            Assert.AreEqual(afList, result);
        }

        [TestMethod]
        public void RemoveAdditionalFieldTest1()
        {
            var guid = Guid.NewGuid();
            var af1 = new AdditionalField() { Id = guid };

            mockAFRepository.Setup(m => m.Get(guid)).Returns(af1);
            mockAFRepository.Setup(m => m.Remove(af1));
            mockAFRepository.Setup(m => m.Save());

            additionalFieldLogic.Remove(guid);

            mockAFRepository.VerifyAll();
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_NAME_NULL_EMPTY")]
        public void CreateInValidAdditionalFieldTest1()
        {
            var af1 = new AdditionalField() { FieldType = "Text" };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_FIELDTYPE_NULL_EMPTY")]
        public void CreateInValidAdditionalFieldTest2()
        {
            var af1 = new AdditionalField() { Name = "AF Name" };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_FIELDTYPE_INVALID")]
        public void CreateInValidAdditionalFieldTest3()
        {
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "not a valid fieldtype" };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_POSSIBLEVALUES_PARSE")]
        public void CreateInValidAdditionalFieldTest4()
        {
            string[] pv1 = { "23/03/1994", "notadate" };
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "Date", PossibleValues = pv1 };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID")]
        public void CreateInValidAdditionalFieldTest5()
        {
            string[] pv1 = { "23/03/1994", "22/03/1994" };
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "Date", PossibleValues = pv1 };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID")]
        public void CreateInValidAdditionalFieldTest6()
        {
            string[] pv1 = { "23/03/1994", "22/03/1994", "22/03/1994" };
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "Date", PossibleValues = pv1 };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID")]
        public void CreateInValidAdditionalFieldTest7()
        {
            string[] pv1 = { "23/03/1994" };
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "Date", PossibleValues = pv1 };
            var result = additionalFieldLogic.Create(af1);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID")]
        public void CreateInValidAdditionalFieldTest8()
        {
            string[] pv1 = { "1" };
            var af1 = new AdditionalField() { Name = "AF Name", FieldType = "Integer", PossibleValues = pv1 };
            var result = additionalFieldLogic.Create(af1);
        }


    }
}