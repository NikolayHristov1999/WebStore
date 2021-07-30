namespace WebStore.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Contact;

    public class ContactService : IContactService
    {
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public ContactService(
            IDeletableEntityRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<int> AddAsync(ContactFormModel model)
        {
            var contact = AutoMapperConfig.MapperInstance.Map<Contact>(model);

            await this.contactRepository.AddAsync(contact);
            await this.contactRepository.SaveChangesAsync();

            return contact.Id;
        }

        public Contact GetContactById(int id)
        {
            return this.contactRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
