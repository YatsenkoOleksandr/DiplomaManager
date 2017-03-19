using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;
using Moq;
using NUnit.Framework;

namespace DiplomaManager.Tests
{
    [TestFixture]
    public class TeacherServiceTests : IDisposable
    {
        private readonly AutoMock _autoMockContext;

        public TeacherServiceTests()
        {
            _autoMockContext = AutoMock.GetLoose();
        }

        [Test]
        public void GetDiplomaRequestsTest()
        {
        }

        public void Dispose()
        {
            _autoMockContext?.Dispose();
        }
    }
}
