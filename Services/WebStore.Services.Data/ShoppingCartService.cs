namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IDeletableEntityRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IProductsService productsService;

        public ShoppingCartService(
            IDeletableEntityRepository<Item> itemRepository,
            IDeletableEntityRepository<Cart> cartRepository,
            IDeletableEntityRepository<Order> orderRepository,
            IProductsService productsService)
        {
            this.itemRepository = itemRepository;
            this.cartRepository = cartRepository;
            this.productsService = productsService;
            this.orderRepository = orderRepository;
        }

        public async Task<T> AddToCartAsync<T>(int cartId, int productId, int quantity)
            where T : class
        {
            var product = this.productsService.GetProductById(productId);
            if (product == null)
            {
                return null;
            }

            var item = this.itemRepository.All()
                .FirstOrDefault(x => x.CartId == cartId && x.ProductId == productId);

            if (item == null)
            {

                item = new Item
                {
                    ProductId = productId,
                    Quantity = quantity,
                    IsPurchased = false,
                    CartId = cartId,
                    ItemTotalPrice = product.Price * quantity,
                };

                await this.itemRepository.AddAsync(item);
            }
            else
            {
                item.Quantity += quantity;
                item.ItemTotalPrice = product.Price * item.Quantity;
            }

            await this.itemRepository.SaveChangesAsync();

            await this.AddItemToCartAsync(cartId, item);

            item.Product = product;

            return AutoMapperConfig.MapperInstance.Map<T>(item);
        }

        public bool CheckQuantity(int productId, int quantity)
        {
            var product = this.productsService.GetProductById(productId);

            if (product == null)
            {
                return false;
            }

            if (!(product.AvailableQuantity >= quantity))
            {
                return false;
            }

            return true;
        }

        public async Task AddItemToCartAsync(int cartId, Item item)
        {
            var cart = this.cartRepository.All().FirstOrDefault(x => x.Id == cartId);
            cart.TotalPrice += item.ItemTotalPrice;
            cart.Items.Add(item);

            await this.cartRepository.SaveChangesAsync();
        }

        public TCart GetCartById<TCart>(int id)
            => this.cartRepository.All()
            .Where(x => x.Id == id)
            .To<TCart>().FirstOrDefault();

        public async Task<int> CreateCartAsync(string userId = null)
        {
            var cart = new Cart();

            if (userId != null)
            {
                cart.UserId = userId;
            }

            await this.cartRepository.AddAsync(cart);
            await this.cartRepository.SaveChangesAsync();

            return cart.Id;
        }

        public IEnumerable<T> GetAllItemsForCartId<T>(int cartId)
        {
            return this.itemRepository.AllAsNoTracking()
                .Where(x => x.CartId == cartId)
                .To<T>()
                .ToList();
        }

        public int GetCartItemsCount(int cartId)
        {
            return this.itemRepository.All()
                .Where(x => x.CartId == cartId)
                .Count();

        }

        public async Task<bool> RemoveItemFromCartAsync(string itemId, int cartId)
        {
            var item = this.itemRepository.All()
                .FirstOrDefault(x => x.Id == itemId && x.CartId == cartId);

            if (item == null)
            {
                return false;
            }

            this.itemRepository.Delete(item);
            await this.itemRepository.SaveChangesAsync();

            // await this.cartRepository.SaveChangesAsync();
            return true;

        }

        public async Task<bool> CreateOrderAsync(CheckoutInputModel model, int cartId, string userId = null)
        {
            var order = AutoMapperConfig.MapperInstance.Map<Order>(model);
            order.CartId = cartId;

            if (userId != null)
            {
                order.UserId = userId;
            }

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return true;
        }

        private Cart GetCartById(int cartId)
        {
            return this.cartRepository.All()
                .Where(x => x.Id == cartId)
                .FirstOrDefault();
        }
    }
}
