
namespace WebStore.Web.ViewModels.Contact
{
    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ContactViewModel : IMapFrom<Contact>, IHaveCustomMappings
    {
        public string ClientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Contact, ContactViewModel>()
                .ForMember(x => x.ClientName, y => y.MapFrom(c => c.FirstName + " " + c.LastName));
        }
    }
}
