using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.WebApi.Controllers;
using Moq;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using IMMRequestDataImport.domain;
using Backend.BusinessLogic.Interface;

namespace Backend.WebApi.Tests
{
    [TestClass]
    public class DataImportControllerTests
    {
        private Mock<IDataImportLogic> mockDILogic;

        private DataImportController dataImportController;

        [TestInitialize]
        public void Setup()
        {
            mockDILogic = new Mock<IDataImportLogic>(MockBehavior.Strict);
            dataImportController = new DataImportController(mockDILogic.Object);
        }

        [TestMethod]
        public void ImportDataTest1()
        {
            var listAreas = new List<Area>() { new Area() };
            mockDILogic.Setup(m => m.Import(It.IsAny<string>(), It.IsAny<List<ImportField>>())).Returns(listAreas);

            var result = dataImportController.ImportData("", "dllName", new List<ImportField>());
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<AreaDT>;

            mockDILogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(IActionResult));
            Assert.AreEqual(1, value.Count);
        }

        [TestMethod]
        public void GetAllTest1()
        {
            var listDllInfo = new List<DllInfo>() { new DllInfo() };
            mockDILogic.Setup(m => m.GetAll()).Returns(listDllInfo);

            var result = dataImportController.getAll("");
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<DllInfoDT>;

            mockDILogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(IActionResult));
            Assert.AreEqual(1, value.Count);
        }

    }
}