namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Services.Data;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(int productId, int quantity)
        {
            if (!this.shoppingCartService.CheckQuantity(productId, quantity))
            {
                return Json(new
                {
                    success = false,
                    message = GlobalConstants.NotEnoughQuantity,
                });
            }

            string id = await this.shoppingCartService.AddToCartAsync(productId, quantity);
            if (id == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Something went wrong while adding the product",
                });
            }

            this.HttpContext.Session.Set(id, )
            return Json()
        }
    }
}
