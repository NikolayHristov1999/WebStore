namespace WebStore.Services.Data
{
    using System.Linq;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;

    public class ContactService : IContactService
    {
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public ContactService(
            IDeletableEntityRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public Contact GetContactById(int id)
        {
            return this.contactRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
