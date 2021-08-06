namespace WebStore.Web.ViewModels.Contact
{
    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ContactOrderViewModel : IMapFrom<Contact>, IHaveCustomMappings
    {
        public string ClientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ShippingStreet { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Contact, ContactOrderViewModel>()
                .ForMember(x => x.ClientName, opt => opt.MapFrom(y => y.FirstName + " " + y.LastName))
                .ForMember(x => x.ClientName, opt => opt.MapFrom(y => y.Street));
        }
    }
}