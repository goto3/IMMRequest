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
    public class TopicTypeRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            TopicType topictype = new TopicType()
            {
                Id = Guid.NewGuid(),
                Name = "TopicType name1",
            };
            using (var context = new BackendContext(options))
            {
                context.Set<TopicType>().Add(topictype);
                context.SaveChanges();
                var manager = new TopicTypeRepository(context);
                List<TopicType> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<TopicType>().Remove(topictype);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            TopicType topictype = new TopicType()
            {
                Id = Guid.NewGuid(),
                Name = "TopicType name1",
            };
            using (var context = new BackendContext(options))
            {
                context.Set<TopicType>().Add(topictype);
                context.SaveChanges();
                var manager = new TopicTypeRepository(context);
                var result = manager.Get(topictype.Id);
                Assert.AreEqual(topictype.Id, result.Id);
                Assert.AreEqual(topictype.Name, result.Name);
                context.Set<TopicType>().Remove(topictype);
                context.SaveChanges();
            }
        }

    }
}
