namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebStore.Common;
    using WebStore.Data;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IDeletableEntityRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<SellerOrder> sellerOrderRepository;
        private readonly IProductsService productsService;

        public ShoppingCartService(
            IDeletableEntityRepository<Item> itemRepository,
            IDeletableEntityRepository<Cart> cartRepository,
            IDeletableEntityRepository<Order> orderRepository,
            IDeletableEntityRepository<SellerOrder> sellerOrderRepository,
            IProductsService productsService)
        {
            this.itemRepository = itemRepository;
            this.cartRepository = cartRepository;
            this.productsService = productsService;
            this.orderRepository = orderRepository;
            this.sellerOrderRepository = sellerOrderRepository;
        }

        public async Task<T> AddToCartAsync<T>(string cartId, int productId, int quantity)
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

        public async Task AddItemToCartAsync(string cartId, Item item)
        {
            var cart = this.cartRepository.All().FirstOrDefault(x => x.Id == cartId);
            cart.TotalPrice += item.ItemTotalPrice;
            cart.Items.Add(item);

            await this.cartRepository.SaveChangesAsync();
        }

        public TCart GetCartById<TCart>(string id)
            => this.cartRepository.All()
            .Where(x => x.Id == id)
            .To<TCart>().FirstOrDefault();

        public async Task<string> CreateCartAsync(string userId = null)
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

        public IEnumerable<T> GetAllItemsForCartId<T>(string cartId)
        {
            return this.itemRepository.AllAsNoTracking()
                .Where(x => x.CartId == cartId)
                .To<T>()
                .ToList();
        }

        public int GetCartItemsCount(string cartId)
        {
            return this.itemRepository.All()
                .Where(x => x.CartId == cartId)
                .Count();
        }

        public async Task<bool> RemoveItemFromCartAsync(string itemId, string cartId)
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

        public async Task<bool> CreateOrderAsync(CheckoutInputModel model, string cartId, string userId = null)
        {
            var sellerOrders = new Dictionary<string, SellerOrder>();
            var order = AutoMapperConfig.MapperInstance.Map<Order>(model);
            order.CartId = cartId;

            if (userId != null)
            {
                order.UserId = userId;
            }

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            var items = this.itemRepository.All()
                .Where(x => x.CartId == cartId)
                .ToList();

            foreach (var item in items)
            {
                var seller = this.productsService.GetProductById(item.ProductId).AddedByUserId;
                if (!sellerOrders.ContainsKey(seller))
                {
                    var sellerOrderTmp = new SellerOrder
                    {
                        SellerId = seller,
                        BuyerId = order.UserId,
                        PaymentMethod = order.PaymentMethod,
                        ShippingMethod = order.ShippingMethod,
                        ContactId = order.ContactId,
                        Status = OrdersStatus.Created.ToString(),
                    };
                    sellerOrders[seller] = sellerOrderTmp;
                }

                item.SellerOrderId = sellerOrders[seller].Id;
                sellerOrders[seller].TotalPrice += item.ItemTotalPrice;
            }

            foreach (var sellerOrder in sellerOrders)
            {
                await this.sellerOrderRepository.AddAsync(sellerOrder.Value);
            }

            await this.sellerOrderRepository.SaveChangesAsync();
            await this.itemRepository.SaveChangesAsync();

            return true;
        }
    }
}
