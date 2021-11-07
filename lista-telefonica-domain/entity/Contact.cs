#nullable enable

namespace lista_telefonica_domain.entity
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }

        public Contact(string id, string name, string? email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
