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
    public class SessionRepositoryTest
    {
        [TestMethod]
        public void GetAllTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            UserSession session = new UserSession()
            {
                Token = Guid.NewGuid()
            };
            using (var context = new BackendContext(options))
            {
                context.Set<UserSession>().Add(session);
                context.SaveChanges();
                var manager = new SessionRepository(context);
                List<UserSession> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<UserSession>().Remove(session);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetTest1()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
            .UseInMemoryDatabase(databaseName: "Db1")
            .Options;

            UserSession session = new UserSession()
            {
                Token = Guid.NewGuid()
            };
            using (var context = new BackendContext(options))
            {
                context.Set<UserSession>().Add(session);
                context.SaveChanges();
                var manager = new SessionRepository(context);
                var result = manager.Get(session.Token);
                Assert.AreEqual(session.Token, result.Token);
                context.Set<UserSession>().Remove(session);
                context.SaveChanges();
            }
        }

    }
}
