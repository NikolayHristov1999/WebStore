namespace WebStore.Web.ViewModels.Administration.Orders
{
    using System;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class TableOrderViewModel : IMapFrom<SellerOrder>, IHaveCustomMappings
    {
        public string SellerOrderId { get; set; }

        public string BuyerId { get; set; }

        public string Name { get; set; }

        public decimal TotalPrice { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SellerOrder, TableOrderViewModel>()
                .ForMember(x => x.SellerOrderId, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Contact.FirstName + " " + y.Contact.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Contact.Email));
        }
    }
}
