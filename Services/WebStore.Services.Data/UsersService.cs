namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Common;
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
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<Dealer> dealerRepository,
            IContactService contactService,
            UserManager<ApplicationUser> userManager)
        {
            this.dealerRepository = dealerRepository;
            this.contactService = contactService;
            this.userManager = userManager;
        }

        public async Task AddUserToRoleAsync(string userId, string role)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Contains(GlobalConstants.DealerRoleName))
            {
                return;
            }

            var result = await this.userManager
                    .AddToRoleAsync(user, GlobalConstants.DealerRoleName);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<string> BecomeDealerAsync(string userId, ContactDealerFormModel model)
        {
            var contactId = await this.contactService
                .AddAsync(AutoMapperConfig.MapperInstance.Map<ContactFormModel>(model));

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
