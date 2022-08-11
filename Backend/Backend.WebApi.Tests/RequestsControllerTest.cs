using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.WebApi.Controllers;
using Backend.BusinessLogic.Interface;
using Moq;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Tools;
using System.Collections.Generic;

namespace Backend.WebApi.Tests
{
    [TestClass]
    public class RequestsControllerTests
    {

        private static Guid ttId1 = new Guid("00000000-0000-0000-0001-000000000000");
        private static ApplicantDT a1 = new ApplicantDT()
        {
            Email = "email@email.com",
            Name = "Name"
        };

        private static TopicType tt1 = new TopicType()
        {
            Id = ttId1,
            Name = "TopicType Name",

        };

        private static AdditionalFieldDataDT af1 = new AdditionalFieldDataDT()
        {
            AdditionalField = Guid.NewGuid(),
            Data = new string[] { "field1 data" }
        };

        private static AdditionalFieldDataDT af2 = new AdditionalFieldDataDT()
        {
            AdditionalField = Guid.NewGuid(),
            Data = new string[] { "field2 data" }
        };

        private static Request r1 = new Request()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000001"),
            Applicant = new Applicant() { Name = "applicant name", Email = "applicant email" },
            Details = "Details",
            Status = "Created",
            StatusDescription = "Status Description",
            TopicType = tt1,
            AdditionalFields = new List<AdditionalFieldData>()
        };

        private static Request r2 = new Request()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000002"),
            Applicant = new Applicant() { Name = "applicant name", Email = "applicant email" },
            Details = "Details",
            Status = "Created",
            StatusDescription = "Status Description",
            TopicType = tt1,
            AdditionalFields = new List<AdditionalFieldData>()
        };

        private RequestsController requestsController;
        private Mock<IRequestLogic> mockRLogic;

        [TestInitialize]
        public void Setup()
        {
            mockRLogic = new Mock<IRequestLogic>(MockBehavior.Strict);

            requestsController = new RequestsController(mockRLogic.Object);
        }

        [TestMethod]
        public void CreateValidRequestTest1()
        {
            var requestModel = new RequestDT
            {
                Applicant = a1,
                Details = "Request details",
                TopicType = ttId1,
                AdditionalFields = new List<AdditionalFieldDataDT>() { af1, af2 },
            };

            mockRLogic.Setup(m => m.Create(It.IsAny<Request>())).Returns(requestModel.ToEntity());

            var result = requestsController.Post(requestModel);
            var createdResult = result as CreatedResult;
            var statusCode = createdResult.StatusCode;

            mockRLogic.VerifyAll();
            Assert.AreEqual(statusCode, 201);
        }

        [TestMethod]
        public void CreateValidRequestTest2()
        {
            var requestModel = new RequestDT { Applicant = new ApplicantDT() };

            mockRLogic.Setup(m => m.Create(It.IsAny<Request>())).Returns(requestModel.ToEntity());

            var result = requestsController.Post(requestModel);
            var createdResult = result as CreatedResult;
            var statusCode = createdResult.StatusCode;

            mockRLogic.VerifyAll();
            Assert.AreEqual(statusCode, 201);
        }

        [TestMethod]
        public void GetAllRequestTest1()
        {
            var list = new List<Request>();

            mockRLogic.Setup(m => m.GetAll()).Returns(list);

            var result = requestsController.GetAll("");
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<RequestDT>;

            mockRLogic.VerifyAll();
            Assert.IsTrue(okResult.Value is List<RequestDT>);
        }

        [TestMethod]
        public void GetAllRequestTest2()
        {
            var list = new List<Request>() { r1, r2 };

            mockRLogic.Setup(m => m.GetAll()).Returns(list);

            var result = requestsController.GetAll("");
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<RequestDT>;

            mockRLogic.VerifyAll();
            Assert.AreEqual(list.Count, value.Count);
        }

        [TestMethod]
        public void GetRequestTest1()
        {
            var guid = Guid.NewGuid();

            mockRLogic.Setup(m => m.Get(guid)).Returns(r1);

            var result = requestsController.Get(guid);
            var okResult = result as ObjectResult;
            var value = okResult.Value as RequestDT;

            mockRLogic.VerifyAll();
            Assert.AreEqual(r1.Status, value.Status);
            Assert.AreEqual(r1.StatusDescription, value.StatusDescription);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_NOT_FOUND")]
        public void GetRequestTest2()
        {
            var guid = Guid.NewGuid();

            mockRLogic.Setup(m => m.Get(guid)).Returns(() => null);

            var result = requestsController.Get(guid);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;

            mockRLogic.VerifyAll();
            Assert.AreEqual(statusCode, 404);
        }

        [TestMethod]
        public void UpdateRequestStatusTest1()
        {
            var guid = Guid.NewGuid();
            var requestStatus = new RequestStatusDT()
            {
                Status = "In review",
                StatusDescription = "New status description"
            };

            mockRLogic.Setup(m => m.UpdateStatus(It.IsAny<Request>()));

            var result = requestsController.Put("", guid, requestStatus);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;

            mockRLogic.VerifyAll();
            Assert.AreEqual(statusCode, 200);
        }

        [TestMethod]
        public void GetApplicantRequestsTest1()
        {
            var email = "applicant@email.com";
            var dt1 = DateTime.Now;
            var dt2 = DateTime.Now.AddMinutes(5);
            var list = new List<Request>() { r1, r2 };

            mockRLogic.Setup(m => m.GetReportA(email, It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(list);

            var result = requestsController.GetReportA("", email, dt1.ToString(), dt2.ToString());
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<RequestDT>;

            mockRLogic.VerifyAll();
            Assert.AreEqual(list.Count, value.Count);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_REQUEST_DATE_FORMAT")]
        public void GetApplicantRequestsInvalidTest1()
        {
            var result = requestsController.GetReportA("", "", "not a valid date", DateTime.Now.ToString());
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<RequestDT>;
        }

    }
}