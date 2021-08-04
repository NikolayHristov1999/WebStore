namespace WebStore.Web.ViewModels.Administration.Dealers
{
    using System;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class TableDealerViewModel : IMapFrom<Dealer>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dealer, TableDealerViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Contact.FirstName + " " + y.Contact.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Contact.Email));
        }
    }
}
