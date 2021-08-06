namespace WebStore.Web.ViewModels.Administration.Orders
{
    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ItemInOrderDetailsViewModel : IMapFrom<Item>
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotalPrice { get; set; }

    }
}