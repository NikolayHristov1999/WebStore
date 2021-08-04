namespace WebStore.Web.ViewModels.Product
{
    using AutoMapper;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ProductInListViewModel : BaseProductViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductInListViewModel>()
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name.Length > GlobalConstants.MaximumLengthNameInView ?
                    x.Name.Substring(0, GlobalConstants.MaximumLengthNameInView) + "..." :
                    x.Name));
        }
    }
}
