using lista_telefonica_domain.entity;

namespace lista_telefonica_domain.service
{
    public interface IContactManager
    {
        Contact Create(Contact customer);
        Contact Find(string contactId);
    }
}
