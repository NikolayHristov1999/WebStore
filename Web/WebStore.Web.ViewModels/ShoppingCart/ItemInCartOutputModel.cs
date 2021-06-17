namespace WebStore.Web.ViewModels.ShoppingCart
{
    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ItemInCartOutputModel : ItemBaseModel, IMapFrom<Item>
    {
        public string ProductImageUrl { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ItemTotalPrice { get; set; }
    }
}
