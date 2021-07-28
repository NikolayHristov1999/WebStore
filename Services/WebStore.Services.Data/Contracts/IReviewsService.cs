namespace WebStore.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<bool> CreateReviewAsync(
            int productId,
            string name,
            string content,
            byte stars,
            string userId);

        IEnumerable<T> GetProductReviews<T>(int productId);
    }
}
