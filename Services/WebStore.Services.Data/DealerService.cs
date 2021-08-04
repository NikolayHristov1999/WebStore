namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Common;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;

    using static WebStore.Data.Common.DataConstants;

    public class DealerService : IDealerService
    {
        private readonly IDeletableEntityRepository<Dealer> dealerRepository;
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IDeletableEntityRepository<SellerOrder> sellerOrderRepository;
        private readonly IUsersService usersService;

        public DealerService(
            IDeletableEntityRepository<Dealer> dealerRepository,
            IDeletableEntityRepository<Item> itemRepository,
            IDeletableEntityRepository<SellerOrder> sellerOrderRepository,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.dealerRepository = dealerRepository;
            this.itemRepository = itemRepository;
            this.sellerOrderRepository = sellerOrderRepository;
            this.usersService = usersService;
        }

        public IEnumerable<Item> GetAllPurchasedItemsForSeller(string userId)
        {
            return this.itemRepository.All()
                .Where(x => x.SellerOrderId != null && x.Product.AddedByUserId == userId)
                .ToList();
        }

        public IEnumerable<SellerOrder> GetAllOrdersForSeller(string userId)
        {
            return this.sellerOrderRepository.All()
                 .Where(x => x.SellerId == userId)
                 .ToList();
        }

        public SellerOrder GetOrderForSellerById(string orderId)
        {
            return this.sellerOrderRepository.All()
                .FirstOrDefault(x => x.Id == orderId);
        }

        public IEnumerable<Item> GetAllItemsBySellerOrderId(string sellerOrderId)
        {
            return this.itemRepository.All()
                .Where(x => x.SellerOrderId == sellerOrderId)
                .ToList();
        }

        public T GetDealerByUserId<T>(string userId)
        {
            return this.dealerRepository.All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetAllDealers<T>()
        {
            return this.dealerRepository.All()
                .To<T>()
                .ToList();
        }

        public bool IsDealerApproved(string userId)
        {
            return this.dealerRepository.All()
                .Where(x => x.UserId == userId)
                .FirstOrDefault()
                .Status.Equals(DealerStatus.Approved.ToString());
        }

        public async Task<bool> ChangeDealerStatusAsync(string userId, string statusInput)
        {
            var isValidStatus = Enum.TryParse(typeof(DealerStatus), statusInput, true, out var status);

            if (!isValidStatus)
            {
                return false;
            }

            var dealer = this.dealerRepository.All()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            if (dealer == null)
            {
                return false;
            }

            dealer.Status = statusInput;

            await this.dealerRepository.SaveChangesAsync();

            if (statusInput == DealerStatus.Approved.ToString())
            {
                await this.usersService.AddUserToRoleAsync(userId, GlobalConstants.DealerRoleName);
            }

            return true;
        }
    }
}
