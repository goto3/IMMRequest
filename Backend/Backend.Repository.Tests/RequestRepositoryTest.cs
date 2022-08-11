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
    public class RequestRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Request request = new Request()
            {
                Id = Guid.NewGuid(),
                Details = "details"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<Request>().Add(request);
                context.SaveChanges();
                var manager = new RequestRepository(context);
                List<Request> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<Request>().Remove(request);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Request request = new Request()
            {
                Id = Guid.NewGuid(),
                Details = "details"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<Request>().Add(request);
                context.SaveChanges();
                var manager = new RequestRepository(context);
                var result = manager.Get(request.Id);
                Assert.AreEqual(request.Id, result.Id);
                Assert.AreEqual(request.Details, result.Details);
                context.Set<Request>().Remove(request);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void UpdateTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            var guid = Guid.NewGuid();
            Request request1 = new Request()
            {
                Id = guid,
                Details = "details"
            };
            using (var context = new BackendContext(options))
            {
                var manager = new RequestRepository(context);
                manager.Add(request1);
                manager.Save();
                var request = manager.Get(guid);
                request.Details = "NewDetails";
                manager.Update(request);
                manager.Save();
                var result = manager.Get(guid);
                Assert.AreEqual(guid, result.Id);
                Assert.AreEqual("NewDetails", result.Details);
                manager.Remove(request1);
                manager.Save();
            }
        }

    }
}
