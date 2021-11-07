using lista_telefonica_domain.entity;

namespace lista_telefonica_domain.repository
{
    public interface IContactRepository
    { 
        void Save(Contact contact);

        Contact Find(string id);
    }
}
