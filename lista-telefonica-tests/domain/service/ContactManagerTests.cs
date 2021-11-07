using AutoFixture;
using FluentAssertions;
using lista_telefonica_domain.entity;
using lista_telefonica_domain.repository;
using lista_telefonica_domain.service;
using lista_telefonica_domain.service.impl;
using Moq;
using System;
using Xunit;

namespace lista_telefonica_tests.domain.service
{
    public class ContactManagerTests
    {
        private readonly Fixture _fixture = new();

        private readonly Mock<IContactRepository> _repository = new();
        private readonly IContactManager _manager;

        private const string FakeValue = "xpto";

        public ContactManagerTests()
        {
            _manager = new ContactManager(_repository.Object);
        }

        [Fact]
        public void Create_GivenAValidContact_ShouldCreateAsExpected()
        {
            var contact = _fixture
                            .Build<Contact>()
                            .Without(c => c.Id)
                            .Create();

            _manager.Create(contact);

            _repository.Verify(r => r.Save(contact), Times.Once());
            _repository.Verify(r => r.Save(It.Is<Contact>(c => !string.IsNullOrEmpty(c.Id))), Times.Once());                                 
        }

        [Fact]
        public void Create_GivenANullContact_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Create(null));
        }

        [Fact]
        public void Find_GivenAnExistentContact_ShouldReturnThis()
        {
            var contact = _fixture.Create<Contact>();

            _repository.Setup(r => r.Find(FakeValue)).Returns(contact);

            var foundedContact = _manager.Find(FakeValue);

            foundedContact.Should().BeEquivalentTo(contact);
        }
    }
}
