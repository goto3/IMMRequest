using Backend.BusinessLogic;
using Backend.Tools;
using IMMRequestDataImport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Backend.DataImport.Tests
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void CreateInstanceImplementingTest1()
        {
            IDataImport objectCreated = ReflectionHandling.CreateInstanceImplementing<IDataImport>(@"JSONImport.dll", "./ImportedDlls/");
            Assert.IsTrue(objectCreated != null);
        }

        [TestMethod]
        public void CreateAllInstancesImplementingTest1()
        {
            List<IDataImport> objectCreated = ReflectionHandling.CreateAllInstancesImplementing<IDataImport>("./ImportedDlls/");
            Assert.IsTrue(objectCreated != null);
            Assert.AreEqual(2, objectCreated.Count);
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_DATAIMPORT_DLL_NOT_FOUND")]
        public void ImportTestInvalid1()
        {
            IDataImport objectCreated = ReflectionHandling.CreateInstanceImplementing<IDataImport>(@"invalid dll name.dll", "./ImportedDlls/");
        }

        [TestMethod]
        [BackendExpectedException(typeof(BackendException), "ERR_DATAIMPORT_DLL_INVALID")]
        public void ImportInvalidTest2()
        {
            IDataImport objectCreated = ReflectionHandling.CreateInstanceImplementing<IDataImport>(@"InvalidTest2.dll", "./ImportedDlls/");
        }

    }
}
