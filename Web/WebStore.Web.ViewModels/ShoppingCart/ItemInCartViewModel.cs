namespace WebStore.Web.ViewModels.ShoppingCart
{
    using AutoMapper;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ItemInCartViewModel : ItemBaseModel, IMapFrom<Item>, IHaveCustomMappings
    {
        public string ProductImageUrl { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ItemTotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, ItemInCartViewModel>()
                .ForMember(x => x.ProductName, opt =>
                    opt.MapFrom(x => x.Product.Name.Length > GlobalConstants.MaximumLengthNameInView ?
                        x.Product.Name.Substring(0, GlobalConstants.MaximumLengthNameInView) + "..." :
                        x.Product.Name));
        }
    }
}
