namespace WebStore.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Messaging;
    using WebStore.Web.ViewModels.ShoppingCart;


    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(
            IShoppingCartService shoppingCartService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody]ItemCartInputModel item)
        {
            var cartId = await this.GetUserCartId();

            var itemInCart = await this.shoppingCartService
                .AddToCartAsync<ItemInCartViewModel>(cartId, item.ProductId, item.Quantity);
            if (itemInCart == null)
            {
                return this.Json(new
                {
                    success = false,
                    message = GlobalConstants.UnavailableProductOrNotEnoughQuantity,
                });
            }

            return this.PartialView(itemInCart);
        }

        public async Task<IActionResult> GetCartItems()
        {
            var cartId = await this.GetUserCartId();

            var cartItems = this.shoppingCartService.GetAllItemsForCartId<ItemInCartViewModel>(cartId);

            var cart = new ItemInCartListViewModel
            {
                Items = cartItems,
            };

            return this.PartialView(cart);
        }

        public async Task<IActionResult> GetCartItemsCount()
        {
            var cartId = await this.GetUserCartId();

            return this.Json(new
            {
                success = true,
                cartItemsCount = this.shoppingCartService.GetCartItemsCount(cartId),
            });
        }

        public async Task<IActionResult> ClearCart()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
            this.HttpContext.Session.SetString(nameof(Cart), cartId);

            return this.Json(new
            {
                success = true,
            });
        }

        public async Task<IActionResult> RemoveCartItem(string id)
        {
            var cartId = await this.GetUserCartId();
            await this.shoppingCartService.RemoveItemFromCartAsync(id, cartId);
            var totalPrice = this.shoppingCartService.GetAllItemsForCartId<ItemInCartViewModel>(cartId)
                    .Sum(x => x.ItemTotalPrice);

            return this.Json(new
            {
                success = true,
                totalPrice = totalPrice,
            });
        }

        public async Task<IActionResult> Checkout()
        {
            var cartId = await this.GetUserCartId();
            var cartItems = this.shoppingCartService.GetAllItemsForCartId<ItemInCartViewModel>(cartId);
            var cart = new ItemInCartListViewModel
            {
                Items = cartItems,
            };

            var checkoutCart = new CheckoutFormModel
            {
                CartItems = cart,
            };
            return this.View(checkoutCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = await this.GetUserCartId();

            if (!await this.shoppingCartService.CreateOrderAsync(model, cartId, user != null ? user.Id : null))
            {
                return this.View(model);
            }

            // await this.emailSender.SendEmailAsync(sender, "WebStore.bg", reciever, "Test", "Content");
            cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
            this.HttpContext.Session.SetString(nameof(Cart), cartId);

            return this.Redirect("/products/all");
        }

        private async Task<string> GetUserCartId()
        {
            var cartId = this.HttpContext.Session.GetString(nameof(Cart));

            if (cartId == null)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
                this.HttpContext.Session.SetString(nameof(Cart), cartId);
            }

            return cartId;
        }
    }
}
