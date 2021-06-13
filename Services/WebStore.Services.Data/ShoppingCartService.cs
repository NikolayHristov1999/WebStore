namespace WebStore.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IProductsService productsService;

        public ShoppingCartService(
            IDeletableEntityRepository<Item> itemRepository,
            IProductsService productsService)
        {
            this.itemRepository = itemRepository;
            this.productsService = productsService;
        }

        public async Task<string> AddToCartAsync(string cartId, int productId, int quantity)
        {
            var product = this.productsService.GetById<Product>(productId);
            if (product == null)
            {
                return null;
            }

            var item = new Item
            {
                ProductId = productId,
                Quantity = quantity,
                IsPurchased = false,
                CartId = cartId,
            };

            await this.itemRepository.AddAsync(item);
            await this.itemRepository.SaveChangesAsync();

            return item.Id;
        }

        public bool CheckQuantity(int productId, int quantity)
        {
            if (!(this.productsService.GetById<Product>(productId).AvailableQuantity >= quantity))
            {
                return false;
            }

            return true;
        }
    }
}
