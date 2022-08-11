using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Backend.Repository;
using Backend.Domain;

namespace Backend.Repository.Tests
{

    [TestClass]
    public class AdditionalFieldDataRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            AdditionalFieldData afield = new AdditionalFieldData()
            {
                Id = Guid.NewGuid(),
                Data = new string[] { "data" }
            };
            using (var context = new BackendContext(options))
            {
                context.Set<AdditionalFieldData>().Add(afield);
                context.SaveChanges();
                var manager = new AdditionalFieldDataRepository(context);
                List<AdditionalFieldData> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<AdditionalFieldData>().Remove(afield);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            AdditionalFieldData afield = new AdditionalFieldData()
            {
                Id = Guid.NewGuid(),
                Data = new string[] { "data" }
            };
            using (var context = new BackendContext(options))
            {
                context.Set<AdditionalFieldData>().Add(afield);
                context.SaveChanges();
                var manager = new AdditionalFieldDataRepository(context);
                var result = manager.Get(afield.Id);
                Assert.AreEqual(afield.Id, result.Id);
                Assert.AreEqual(afield.Data, result.Data);
                context.Set<AdditionalFieldData>().Remove(afield);
                context.SaveChanges();
            }
        }

    }
}
