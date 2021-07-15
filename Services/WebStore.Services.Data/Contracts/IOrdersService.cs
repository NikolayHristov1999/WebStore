namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using WebStore.Data.Models;

    public interface IOrdersService
    {
        IEnumerable<Order> GetAllOrders();

        IEnumerable<Order> GetAllOrdersForUser(string userId);
    }
}
