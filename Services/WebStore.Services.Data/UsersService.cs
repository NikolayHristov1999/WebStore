namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Contact;

    using static WebStore.Data.Common.DataConstants;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Dealer> dealerRepository;
        private readonly IContactService contactService;

        public UsersService(
            IDeletableEntityRepository<Dealer> dealerRepository,
            IContactService contactService)
        {
            this.dealerRepository = dealerRepository;
            this.contactService = contactService;
        }

        public async Task<string> BecomeDealerAsync(string userId, ContactFormModel model)
        {
            var contactId = await this.contactService.AddAsync(model);
            var dealer = new Dealer
            {
                UserId = userId,
                ContactId = contactId,
                Status = DealerStatus.Pending.ToString(),
            };

            await this.dealerRepository.AddAsync(dealer);
            await this.dealerRepository.SaveChangesAsync();

            return dealer.Id;
        }

        public IEnumerable<T> GetPendingDealers<T>()
        {
            return this.dealerRepository.All()
                .Where(x => x.Status == DealerStatus.Pending.ToString())
                .To<T>();
        }

        public bool IsDealer(string userId)
        {
            return this.dealerRepository.All()
                .Any(x => x.UserId == userId);
        }
    }
}
