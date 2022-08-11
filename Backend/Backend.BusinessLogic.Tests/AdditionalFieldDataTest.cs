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
    public class AdditionalFieldDataTest
    {

        private static AdditionalField af1 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0001-000000000000"),
            FieldType = "Text",
            PossibleValues = new string[] { "text1", "text2" }
        };

        private static AdditionalField af2 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0002-000000000000"),
            FieldType = "Date",
            PossibleValues = new string[] { "23/03/1994", "23/03/2020" }
        };

        private static AdditionalField af3 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0003-000000000000"),
            FieldType = "Integer",
            PossibleValues = new string[] { "1", "10" }
        };

        private static AdditionalField af4 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0003-000000000000"),
            FieldType = "Integer",
            PossibleValues = null
        };

        private static AdditionalField af5 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0005-000000000000"),
            FieldType = "Text",
            MultipleValues = true,
            PossibleValues = new string[] { "text1", "text2", "text3" }
        };

        private ILogic<AdditionalFieldData> afdLogic;
        private Mock<IRepository<AdditionalFieldData>> mockAFDRepo;
        private Mock<ILogic<AdditionalField>> mockAFLogic;

        [TestInitialize]
        public void Setup()
        {
            mockAFDRepo = new Mock<IRepository<AdditionalFieldData>>(MockBehavior.Strict);
            mockAFLogic = new Mock<ILogic<AdditionalField>>(MockBehavior.Strict);

            afdLogic = new AdditionalFieldDataLogic(mockAFDRepo.Object, mockAFLogic.Object);
        }

        [TestMethod]
        public void CreateValidTest1()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af1,
                Data = new string[] { "text1" },
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af1.Id)).Returns(af1);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, field);
        }

        [TestMethod]
        public void CreateValidTest2()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af2,
                Data = new string[] { "23/03/1996" },
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af2.Id)).Returns(af2);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, field);
        }

        [TestMethod]
        public void CreateValidTest3()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af3,
                Data = new string[] { "6" },
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af3.Id)).Returns(af3);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, field);
        }

        [TestMethod]
        public void CreateValidTest4()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af4,
                Data = new string[] { "6" },
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af4.Id)).Returns(af4);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, field);
        }

        [TestMethod]
        public void CreateValidTest5()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af5,
                Data = new string[] { "text1", "text2" },
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af5.Id)).Returns(af5);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, field);
        }

        [TestMethod]
        public void GetAdditionalFieldTest1()
        {
            var guid = Guid.NewGuid();
            var afd = new AdditionalFieldData() { Id = guid };

            mockAFDRepo.Setup(m => m.Get(guid)).Returns(afd);

            var result = afdLogic.Get(guid);

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, afd);
        }

        [TestMethod]
        public void GetAllAdditionalFieldTest1()
        {
            var afdList = new List<AdditionalFieldData>();

            mockAFDRepo.Setup(m => m.GetAll()).Returns(afdList);

            var result = afdLogic.GetAll();

            mockAFDRepo.VerifyAll();
            Assert.AreEqual(result, afdList);
        }

        [TestMethod]
        public void RemoveAdditinoalFieldTest1()
        {
            var guid = Guid.NewGuid();
            var afd = new AdditionalFieldData() { Id = guid };

            mockAFDRepo.Setup(m => m.Get(guid)).Returns(afd);
            mockAFDRepo.Setup(m => m.Remove(afd));
            mockAFDRepo.Setup(m => m.Save());

            afdLogic.Remove(guid);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiled null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_FIELD_NULL_NOT_FOUND")]
        public void CreateInValidTest1()
        {
            var field = new AdditionalFieldData
            {
                Data = new string[] { "field data" },
            };
            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af1.Id)).Returns(af1);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_DATA_NULL_EMPTY")]
        public void CreateInValidTest2()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af1,
                Data = null
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af1.Id)).Returns(af1);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData empty
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_DATA_NULL_EMPTY")]
        public void CreateInValidTest3()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af1,
                Data = new string[] { "" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af1.Id)).Returns(af1);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_INVALID")]
        public void CreateInValidTest4()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af2,
                Data = new string[] { "not a date" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af2.Id)).Returns(af2);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_INVALID")]
        public void CreateInValidTest5()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af3,
                Data = new string[] { "not an integer" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af3.Id)).Returns(af3);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_OUTOFRANGE")]
        public void CreateInValidTest6()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af1,
                Data = new string[] { "not in possible values" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af1.Id)).Returns(af1);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_OUTOFRANGE")]
        public void CreateInValidTest7()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af2,
                Data = new string[] { "23/03/1993" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af2.Id)).Returns(af2);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_OUTOFRANGE")]
        public void CreateInValidTest8()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af2,
                Data = new string[] { "23/03/2021" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af2.Id)).Returns(af2);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }

        [TestMethod] //AdditionalFiledData null
        [BackendExpectedException(typeof(BackendException), "ERR_ADDITIONALFIELDDATA_OUTOFRANGE")]
        public void CreateInValidTest9()
        {
            var field = new AdditionalFieldData
            {
                AdditionalField = af3,
                Data = new string[] { "11" }
            };

            mockAFDRepo.Setup(m => m.Add(It.IsAny<AdditionalFieldData>()));
            mockAFDRepo.Setup(m => m.Save());
            mockAFLogic.Setup(m => m.Get(af3.Id)).Returns(af3);

            var result = afdLogic.Create(field);

            mockAFDRepo.VerifyAll();
        }



    }
}

