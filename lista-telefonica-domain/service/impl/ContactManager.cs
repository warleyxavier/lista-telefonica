using lista_telefonica_domain.entity;
using lista_telefonica_domain.repository;
using System;

namespace lista_telefonica_domain.service.impl
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository _repository;

        public ContactManager(IContactRepository repository)
        {
            _repository = repository;
        }

        public Contact Create(Contact customer)
        {
            ValidateContact(customer);

            customer.Id = GenerateId();

            _repository.Save(customer);

            return customer;
        }

        private void ValidateContact(Contact contact)
        {
            if (contact is null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public Contact Find(string contactId)
        {
            return _repository.Find(contactId);
        }
    }
}
