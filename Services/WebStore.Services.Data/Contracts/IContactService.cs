
namespace WebStore.Services.Data.Contracts
{
    using WebStore.Data.Models;

    public interface IContactService
    {
        Contact GetContactById(int id);
    }
}
