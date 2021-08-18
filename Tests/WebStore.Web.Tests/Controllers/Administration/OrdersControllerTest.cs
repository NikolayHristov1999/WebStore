namespace WebStore.Web.Tests.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Web.Areas.Administration.Controllers;
    using WebStore.Web.ViewModels.Administration.Orders;
    using Xunit;

    using static WebStore.Web.Tests.Data.SellerOrders;

    public class OrdersControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewModelWithProducts()
            => MyController<OrdersController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetTenSellerOrdersWithDealerId(TestUser.Identifier)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<TableOrderViewModel>>());
    }
}
