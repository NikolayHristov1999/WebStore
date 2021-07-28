namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task<bool> CreateReviewAsync(
            int productId,
            string name,
            string content,
            byte stars,
            string userId)
        {
            var review = new Review
            {
                ProductId = productId,
                Name = name,
                Content = content,
                Stars = stars,
                UserId = userId,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();

            return true;
        }

        public IEnumerable<T> GetProductReviews<T>(int productId)
        {
            return this.reviewsRepository.AllAsNoTracking()
                .Where(x => x.ProductId == productId)
                .To<T>();
        }
    }
}
