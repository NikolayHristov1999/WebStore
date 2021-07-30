
namespace WebStore.Web.ViewModels.Contact
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ContactViewModel : IMapFrom<Contact>
    {
        public string ClientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }
    }
}
