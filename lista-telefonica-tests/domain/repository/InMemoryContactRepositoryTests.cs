using AutoFixture;
using FluentAssertions;
using lista_telefonica_domain.entity;
using lista_telefonica_domain.repository.impl;
using System;
using System.Collections.Generic;
using Xunit;

namespace lista_telefonica_tests.domain.repository
{
    public class InMemoryContactRepositoryTests
    {
        private readonly Fixture _fixture = new();
        private List<Contact> _contacts;
        
        private const string FakeValue = "xpto";

        [Fact]
        public void Insert_GivenAnContact_ShouldInsertAsExpected()
        {
            var contact = _fixture.Create<Contact>();

            var repository = BuildRepository();

            repository.Insert(contact);

            ValidateContacts(1, contact);
        }

        [Fact]
        public void Update_GivenAInexistentContact_ShouldThrowInvalidOperationException()
        {
            var contact = _fixture.Create<Contact>();

            var repository = BuildRepository();

            var exception = Record.Exception(() => repository.Update(contact));

            exception.GetType().Name.Should().Be(nameof(InvalidOperationException));
        }

        [Fact]
        public void Update_GivenAExistentContact_ShouldUpdateAsExpected()
        {
            var contact = _fixture.Create<Contact>();

            var repository = BuildRepository();

            repository.Insert(contact);

            contact.Name = FakeValue;

            repository.Update(contact);

            ValidateContacts(1, contact);
        }

        [Fact]
        public void Save_GivenAnContactSavedMultipleTimes_ShouldSaveAsExpected()
        {
            var contact = _fixture.Create<Contact>();

            var repository = BuildRepository();

            repository.Save(contact);

            ValidateContacts(1, contact);

            var changedContact = _fixture
                                    .Build<Contact>()
                                    .With(c => c.Id, contact.Id)
                                    .Create();

            repository.Save(changedContact);

            ValidateContacts(1, changedContact);

            var newContact = _fixture.Create<Contact>();

            repository.Save(newContact);

            ValidateContacts(2, newContact);
        }

        [Fact]
        public void Find_GivenAInexistentId_ShouldReturnNull()
        {
            var repository = BuildRepository();

            var contact = repository.Find(FakeValue);

            contact.Should().BeNull();
        }

        [Fact]
        public void Find_GivenAExistentId_ShouldReturnContactAsExpected()
        {
            var contact = _fixture.Create<Contact>();

            var repository = BuildRepository();

            repository.Insert(contact);

            var findedContact = repository.Find(contact.Id);

            findedContact.Should().BeEquivalentTo(contact);
        }

        private InMemoryContactRepository BuildRepository()
        {
            _contacts = new List<Contact>();

            return new InMemoryContactRepository(_contacts);
        }

        private void ValidateContacts(int expectedCount, Contact contactToCompare)
        {
            _contacts.Count.Should().Be(expectedCount);

            var index = _contacts.FindIndex(c => c.Id == contactToCompare.Id);

            index.Should().BeGreaterThanOrEqualTo(0);

            _contacts[index].Should().BeEquivalentTo(contactToCompare);
        }
    }
}
