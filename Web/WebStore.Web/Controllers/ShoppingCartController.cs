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
            const string Cart = "cart";
            int? cartId;
            if (!this.shoppingCartService.CheckQuantity(item.ProductId, item.Quantity))
            {
                return Json(new
                {
                    success = false,
                    message = GlobalConstants.NotEnoughQuantity,
                });
            }


            if (!this.HttpContext.Session.Keys.Contains(Cart))
            {
                var user = await this.userManager.GetUserAsync(this.User);
                cartId = await this.shoppingCartService.CreateCartAsync(user != null ? user.Id : null);
                this.HttpContext.Session.SetInt32(Cart, (int)cartId);
            }
            else
            {
                cartId = this.HttpContext.Session.GetInt32(Cart);
            }

            var itemInCart = await this.shoppingCartService
                .AddToCartAsync<ItemInCartOutputModel>((int)cartId, item.ProductId, item.Quantity);
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
    }
}
