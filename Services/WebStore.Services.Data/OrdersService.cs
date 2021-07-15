namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.ordersRepository.All()
                .ToList();
        }


        public IEnumerable<Order> GetAllOrdersForUser(string userId)
        {
            return this.ordersRepository.All()
                .Where(x => x.UserId == userId)
                .ToList();
        }
    }
}
