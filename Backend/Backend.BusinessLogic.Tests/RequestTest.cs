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
    public class RequestTest
    {
        private static Applicant aplOK = new Applicant
        {
            Email = "applicant@email.com",
            Name = "Applicant name",
            PhoneNumber = "Applicant Phone Number"
        };
        private static AdditionalField af1 = new AdditionalField
        {
            Id = new Guid("00000000-0000-0000-0001-000000000000"),
            FieldType = "Text"
        };
        private static TopicType tt1 = new TopicType
        {
            Id = new Guid("00000000-0000-0000-0000-000000000001"),
            Name = "Topic Type name"
        };
        private static TopicType tt2 = new TopicType
        {
            Id = new Guid("00000000-0000-0000-0000-000000000002"),
            AdditionalFields = new List<AdditionalField>() { af1 },
            Name = "Topic Type name"
        };
        private static AdditionalFieldData afd1 = new AdditionalFieldData
        {
            Id = new Guid("00000000-0000-0001-0000-000000000000"),
            AdditionalField = af1,
            Data = new string[] { "af1 data" },
        };

        private IRequestLogic requestLogic;
        private Mock<IRepository<Request>> mockRRepository;
        private Mock<ILogic<AdditionalFieldData>> mockAFDLogic;
        private Mock<ITopicTypeLogic> mockTTLogic;

        [TestInitialize]
        public void Setup()
        {
            mockRRepository = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mockRRepository.Setup(m => m.Add(It.IsAny<Request>()));
            mockRRepository.Setup(m => m.Save());

            mockAFDLogic = new Mock<ILogic<AdditionalFieldData>>(MockBehavior.Strict);
            mockAFDLogic.Setup(m => m.Validate(It.IsAny<AdditionalFieldData>()));

            mockTTLogic = new Mock<ITopicTypeLogic>(MockBehavior.Strict);

            requestLogic = new RequestLogic(mockRRepository.Object, mockTTLogic.Object, mockAFDLogic.Object);
        }

        [TestMethod]
        public void CreateValidRequestTest1()
        {
            var request = new Request
            {
                Applicant = aplOK,
                TopicType = tt1,
                Details = "Request details"
            };

            mockTTLogic.Setup(m => m.Get(tt1.Id)).Returns(tt1);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockTTLogic.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void CreateValidRequestTest2()
        {
            List<AdditionalFieldData> afList = new List<AdditionalFieldData>() { afd1 };
            var request = new Request
            {
                Applicant = aplOK,
                TopicType = tt2,
                AdditionalFields = afList,
                Details = "Request details"
            };

            mockAFDLogic.Setup(m => m.Create(It.IsAny<AdditionalFieldData>())).Returns(afd1);
            mockTTLogic.Setup(m => m.Get(tt2.Id)).Returns(tt2);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockAFDLogic.VerifyAll();
            mockTTLogic.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod] //Empty request data
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_DETAILS_NULL_EMPTY")]
        public void CreateInvalidRequestTest1()
        {
            var request = new Request() { TopicType = tt1, Applicant = aplOK, };

            mockTTLogic.Setup(m => m.Get(tt1.Id)).Returns(tt1);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockTTLogic.VerifyAll();
        }

        [TestMethod] //null TopicType
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_TOPICTYPE_NULL_NOTFOUND")]
        public void CreateInvalidRequestTest2()
        {
            var request = new Request() { Applicant = aplOK, Details = "Request details" };

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
        }

        [TestMethod] //TopicType not found
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_TOPICTYPE_NULL_NOTFOUND")]
        public void CreateInvalidRequestTest3()
        {
            var request = new Request { Applicant = aplOK, TopicType = tt1, Details = "Request details" };

            mockTTLogic.Setup(m => m.Get(tt1.Id)).Returns(() => null);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockTTLogic.VerifyAll();
        }

        [TestMethod] //Missing Additional fields
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_MISSING_ADDITIONALFIELD")]
        public void CreateInvalidRequestTest4()
        {
            var request = new Request
            {
                Applicant = aplOK,
                TopicType = tt2,
                Details = "Request details"
            };

            mockTTLogic.Setup(m => m.Get(tt2.Id)).Returns(tt2);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockTTLogic.VerifyAll();
        }

        [TestMethod] //Missing Additional fields
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_TOO_MANY_ADDITIONALFIELDS")]
        public void CreateInvalidRequestTest5()
        {
            List<AdditionalFieldData> afList = new List<AdditionalFieldData>() { afd1, afd1 };
            var request = new Request
            {
                Applicant = aplOK,
                TopicType = tt2,
                Details = "Request details",
                AdditionalFields = afList
            };

            mockTTLogic.Setup(m => m.Get(tt2.Id)).Returns(tt2);

            var result = requestLogic.Create(request);

            mockRRepository.VerifyAll();
            mockTTLogic.VerifyAll();
        }

        [TestMethod] //Details length > 2000
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_DETAILS_LONG")]
        public void CreateInvalidRequestTest6()
        {
            var longString = "AnyString";
            for (int i = 0; i < 8; i++)
            {
                longString = longString + longString;
            }
            var request = new Request() { Applicant = aplOK, TopicType = tt1, Details = longString };

            mockTTLogic.Setup(m => m.Get(tt1.Id)).Returns(tt1);

            var result = requestLogic.Create(request);
        }

        [TestMethod]
        public void GetRequestTest1()
        {
            var guid = new Guid("00000000-0000-0000-0000-000000000001");
            var r1 = new Request() { Id = guid };

            mockRRepository.Setup(m => m.Get(guid)).Returns(r1);

            var result = requestLogic.Get(guid);

            mockRRepository.Verify(m => m.Get(guid));
            Assert.AreEqual(r1, result);
        }

        [TestMethod]
        public void GetAllRequestTest1()
        {
            var list = new List<Request>();
            mockRRepository.Setup(m => m.GetAll()).Returns(list);

            var result = requestLogic.GetAll();

            mockRRepository.Verify(m => m.GetAll());
            Assert.AreEqual(list, result);
        }

        [TestMethod]
        public void UpdateRequestTest1()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Created",
                StatusDescription = "Status description"
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "In review",
                StatusDescription = "New status description"
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);
            mockRRepository.Setup(m => m.Update(req1));
            mockRRepository.Setup(m => m.Save());

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
            mockRRepository.Verify(m => m.Save());
        }

        [TestMethod]
        public void UpdateRequestTest2()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Accepted",
                StatusDescription = "Status description"
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Accepted",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);
            mockRRepository.Setup(m => m.Update(req1));
            mockRRepository.Setup(m => m.Save());

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
            mockRRepository.Verify(m => m.Save());
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_NOT_FOUND")]
        public void UpdateInvalidRequestTest1()
        {
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "In review",
                StatusDescription = "New status description"
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(() => null);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_NULL")]
        public void UpdateInvalidRequestTest2()
        {
            requestLogic.UpdateStatus(null);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_STATUS_UPDATE_PRECEDENCE")]
        public void UpdateInvalidRequestTest3()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Created",
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Accepted",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_STATUS_UPDATE_PRECEDENCE")]
        public void UpdateInvalidRequestTest4()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "In review",
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Closed",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_STATUS_UPDATE_PRECEDENCE")]
        public void UpdateInvalidRequestTest5()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Accepted",
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Created",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_STATUS_UPDATE_PRECEDENCE")]
        public void UpdateInvalidRequestTest6()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Closed",
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "In review",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_STATUS_PARSE")]
        public void UpdateInvalidRequestTest7()
        {
            var req1 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "Closed",
            };
            var req2 = new Request()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Status = "not a valid status",
            };
            mockRRepository.Setup(m => m.Get(req2.Id)).Returns(req1);

            requestLogic.UpdateStatus(req2);

            mockRRepository.Verify(m => m.Get(req2.Id));
        }

        [TestMethod]
        public void RemoveRequestTest1()
        {
            var req = new Request() { Id = Guid.NewGuid() };

            mockRRepository.Setup(m => m.Get(req.Id)).Returns(req);
            mockRRepository.Setup(m => m.Remove(req));

            requestLogic.Remove(req.Id);

            mockRRepository.Verify(m => m.Get(req.Id));
            mockRRepository.Verify(m => m.Remove(req));
            mockRRepository.Verify(m => m.Save());
        }

        [TestMethod]
        public void GetApplicantRequestsTest1()
        {
            var dt1 = DateTime.Parse("23/03/1994 10:10:10");
            var r1 = new Request() { Applicant = aplOK, Date = dt1 };
            var r2 = new Request() { Applicant = aplOK, Date = dt1.AddMinutes(5) };
            var r3 = new Request() { Applicant = new Applicant() { Email = "apl2@email.com" }, Date = dt1.AddMinutes(-30) };

            var list = new List<Request>() { r1, r2, r3 };
            mockRRepository.Setup(m => m.GetAll()).Returns(list);

            var result = (List<Request>)requestLogic.GetReportA(aplOK.Email, dt1.AddMinutes(-1), dt1.AddMinutes(1));

            mockRRepository.Verify(m => m.GetAll());
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_DATES_INVALID")]
        public void GetApplicantRequestsInvalidTest1()
        {
            var dt1 = DateTime.Parse("23/03/1994 10:10:10");
            var r1 = new Request() { Applicant = aplOK, Date = dt1 };

            var list = new List<Request>() { r1 };
            mockRRepository.Setup(m => m.GetAll()).Returns(list);

            var result = (List<Request>)requestLogic.GetReportA(aplOK.Email, dt1.AddMinutes(1), dt1.AddMinutes(-1));

            mockRRepository.Verify(m => m.GetAll());
        }


    }
}