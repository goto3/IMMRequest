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
    public class AdditionalFieldRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            AdditionalField afield = new AdditionalField()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<AdditionalField>().Add(afield);
                context.SaveChanges();
                var manager = new AdditionalFieldRepository(context);
                List<AdditionalField> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<AdditionalField>().Remove(afield);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            AdditionalField afield = new AdditionalField()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<AdditionalField>().Add(afield);
                context.SaveChanges();
                var manager = new AdditionalFieldRepository(context);
                var result = manager.Get(afield.Id);
                Assert.AreEqual(afield.Id, result.Id);
                Assert.AreEqual(afield.Name, result.Name);
                context.Set<AdditionalField>().Remove(afield);
                context.SaveChanges();
            }
        }

    }
}
