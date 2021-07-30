﻿namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;

    public class SalesmanService : ISalesmanService
    {
        private readonly IDeletableEntityRepository<Dealer> dealerRepository;
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IDeletableEntityRepository<SellerOrder> sellerOrderRepository;

        public SalesmanService(
            IDeletableEntityRepository<Dealer> dealerRepository,
            IDeletableEntityRepository<Item> itemRepository,
            IDeletableEntityRepository<SellerOrder> sellerOrderRepository)
        {
            this.dealerRepository = dealerRepository;
            this.itemRepository = itemRepository;
            this.sellerOrderRepository = sellerOrderRepository;
        }

        public Task AddUserToRole(UserManager<ApplicationUser> user, string role)
        {
            throw new NotImplementedException();
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
    }
}
