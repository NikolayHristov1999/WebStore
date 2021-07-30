namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WebStore.Web.ViewModels.Contact;

    public interface IUsersService
    {
        Task<string> BecomeDealerAsync(string userId, ContactFormModel model);

        bool IsDealer(string userId);

        IEnumerable<T> GetPendingDealers<T>();
    }
}
