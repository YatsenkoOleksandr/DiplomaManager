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
            Mock_DiplomaManagerUnitOfWork();
        }

        private void Mock_DiplomaManagerUnitOfWork()
        {
            _autoMockContext.Mock<IDiplomaManagerUnitOfWork>().Setup(method =>
                method.FirstNames.Get(
                    It.IsAny<FilterExpression<FirstName>>(), 
                    It.IsAny<IncludeExpression<FirstName>[]>()
                )
            )
            .Returns(new List<FirstName>
            {
                new FirstName
                {
                    CreationDate = DateTime.Now,
                    Id = 1,
                    Locale = new Locale { Id = 1, Name = "English", NativeName = "English" },
                    LocaleId = 1,
                    Name = "Luchshev"
                },
                new FirstName
                {
                    CreationDate = DateTime.Now,
                    Id = 1,
                    Locale = new Locale { Id = 1, Name = "Russian", NativeName = "Русский" },
                    LocaleId = 1,
                    Name = "Лучшев"
                }
            });
        }

        public void Dispose()
        {
            _autoMockContext?.Dispose();
        }
    }
}
