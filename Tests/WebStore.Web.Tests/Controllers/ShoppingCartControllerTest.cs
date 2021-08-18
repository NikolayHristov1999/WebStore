namespace WebStore.Web.Tests.Controllers
{
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using WebStore.Web.Controllers;
    using WebStore.Web.ViewModels.ShoppingCart;
    using Xunit;

    using static WebStore.Web.Tests.Data.Products;

    public class ShoppingCartControllerTest
    {
        [Fact]
        public void AddCartItemShouldReturnPartialViewIfQuantityIsEnough()
            => MyController<ShoppingCartController>
                .Instance(controller => controller
                    .WithData(FifteenProducts))
                .Calling(c => c.AddCartItem(new ItemCartInputModel
                {
                    ProductId = 1,
                    Quantity = 2,
                }))
                .ShouldReturn()
                .PartialView(view => view
                    .WithModelOfType<ItemInCartViewModel>());

        [Fact]
        public void GetCartItemsShouldReturnPartialView()
            => MyController<ShoppingCartController>
                .Instance()
                .Calling(c => c.GetCartItems())
                .ShouldReturn()
                .PartialView(view => view
                    .WithModelOfType<ItemInCartListViewModel>());

        [Fact]
        public void ClearCartShouldReturnJson()
            => MyController<ShoppingCartController>
                .Instance()
                .Calling(c => c.ClearCart())
                .ShouldReturn()
                .Json();

        [Fact]
        public void CheckoutShouldReturnViewWithModel()
            => MyController<ShoppingCartController>
                .Instance()
                .Calling(c => c.Checkout())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CheckoutFormModel>());

    }
}
