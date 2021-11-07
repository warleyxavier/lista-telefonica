using lista_telefonica_domain.entity;
using System.Collections.Generic;
using System.Linq;

namespace lista_telefonica_domain.repository.impl
{
    public class InMemoryContactRepository : IContactRepository
    {
        private List<Contact> _contacts;

        public InMemoryContactRepository()
        {
            _contacts = new List<Contact>();
        }

        public Contact Find(string id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public void Save(Contact contact)
        {
            var alreadyExists = _contacts.Any(c => c.Id == contact.Id);

            if (alreadyExists)
            {
                Update(contact);
                return;
            }

            Insert(contact);
        }

        public void Update(Contact contact)
        {
            var contactIndex = _contacts.FindIndex(c => c.Id == contact.Id);

            _contacts[contactIndex] = contact;
        }

        public void Insert(Contact contact)
        {
            _contacts.Add(contact);
        }
    }
}
