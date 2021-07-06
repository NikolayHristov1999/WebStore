 namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.ShoppingCart;


    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(
            IShoppingCartService shoppingCartService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Route("/[controller]/AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody]ItemCartInputModel item)
        {
            if (!this.shoppingCartService.CheckQuantity(item.ProductId, item.Quantity))
            {
                return Json(new
                {
                    success = false,
                    message = GlobalConstants.NotEnoughQuantity,
                });
            }


            var cartId = await this.GetUserCartId();

            var itemInCart = await this.shoppingCartService
                .AddToCartAsync<ItemInCartViewModel>(cartId, item.ProductId, item.Quantity);
            if (itemInCart == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Something went wrong while adding the product",
                });
            }

            return this.PartialView(itemInCart);

            // return Json(new { success = true });
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
            this.HttpContext.Session.SetInt32(nameof(Cart), cartId);

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

            var checkoutCart = new CheckoutInputModel
            {
                CartItems = cart,
            };
            return this.View(checkoutCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutInputModel model)
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

            cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
            this.HttpContext.Session.SetInt32(nameof(Cart), (int)cartId);
            return this.Redirect("/products/all");
        }

        private async Task<int> GetUserCartId()
        {
            var cartId = this.HttpContext.Session.GetInt32(nameof(Cart));

            if (cartId == null)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
                this.HttpContext.Session.SetInt32(nameof(Cart), (int)cartId);
            }

            return (int)cartId;
        }
    }
}
