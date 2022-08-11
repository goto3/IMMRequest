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
    public class TopicRepositoryTest
    {

        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Topic topic = new Topic()
            {
                Id = Guid.NewGuid(),
                Name = "TopicType name1",
            };
            using (var context = new BackendContext(options))
            {
                var manager = new TopicRepository(context);
                manager.Add(topic);
                manager.Save();
                List<Topic> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                manager.Remove(topic);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            Topic topic = new Topic()
            {
                Id = Guid.NewGuid(),
                Name = "TopicType name1",
            };
            using (var context = new BackendContext(options))
            {
                context.Set<Topic>().Add(topic);
                context.SaveChanges();
                var manager = new TopicRepository(context);
                var result = manager.Get(topic.Id);
                Assert.AreEqual(topic.Id, result.Id);
                Assert.AreEqual(topic.Name, result.Name);
                context.Set<Topic>().Remove(topic);
                context.SaveChanges();
            }
        }
    }
}
