
namespace WebStore.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.Contact;

    public interface IContactService
    {
        Contact GetContactById(int id);

        Task<int> AddAsync(ContactFormModel model);
    }
}
