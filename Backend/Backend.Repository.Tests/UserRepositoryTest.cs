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
    public class UserRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "User name"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<User>().Add(user);
                context.SaveChanges();
                var manager = new UserRepository(context);
                List<User> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<User>().Remove(user);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "User name"
            };
            using (var context = new BackendContext(options))
            {
                context.Set<User>().Add(user);
                context.SaveChanges();
                var manager = new UserRepository(context);
                var result = manager.Get(user.Id);
                Assert.AreEqual(user.Id, result.Id);
                Assert.AreEqual(user.Name, result.Name);
                context.Set<User>().Remove(user);
                context.SaveChanges();
            }
        }

    }
}
