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
    public class AreaRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Area area = new Area()
            {
                Id = Guid.NewGuid(),
                Name = "Area name1",
            };
            using (var context = new BackendContext(options))
            {
                context.Set<Area>().Add(area);
                context.SaveChanges();
                var manager = new AreaRepository(context);
                List<Area> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<Area>().Remove(area);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Area area = new Area()
            {
                Id = Guid.NewGuid(),
                Name = "Area name1"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<Area>().Add(area);
                context.SaveChanges();
                var manager = new AreaRepository(context);
                var result = manager.Get(area.Id);
                Assert.AreEqual(area.Id, result.Id);
                Assert.AreEqual(area.Name, result.Name);
                context.Set<Area>().Remove(area);
                context.SaveChanges();
            }
        }

    }
}
