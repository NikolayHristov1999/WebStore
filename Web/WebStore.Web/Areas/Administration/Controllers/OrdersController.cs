namespace WebStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Administration.Orders;

    public class OrdersController : AdministrationController
    {
        private readonly IDealerService dealerService;

        public OrdersController(
            IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        public IActionResult Index()
        {
            var orders = this.dealerService.GetAllOrdersForSeller<TableOrderViewModel>(this.User.GetId())
               .OrderByDescending(x => x.CreatedOn);

            return this.View(orders);
        }

        public IActionResult ById(string id)
        {
            var userId = this.User.GetId();
            var model = this.dealerService.SellerOrderById<DetailsOrderViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            if (!(this.User.IsInRole("Administrator") || userId == model.SellerId))
            {
                return this.Unauthorized();
            }

            return this.View(model);
        }

        public IActionResult LastDaysSalesCount(int lastDays)
        {
            var days = new List<string>();
            var sales = new List<int>();

            for (var i = 0; i < lastDays; i++)
            {
                var date = DateTime.UtcNow.AddDays(i * -1);
                var ordersCount = this.dealerService.CountOfSalesForADay(this.User.GetId(), date);

                days.Add(date.ToString("dd/MM"));
                sales.Add(ordersCount);
            }

            var jsonModel = new
            {
                success = true,
                Days = days,
                Sales = sales,
            };

            return this.Json(jsonModel);
        }

        public IActionResult LastDaysSalesAmount(int lastDays)
        {
            var days = new List<string>();
            var sales = new List<decimal>();

            for (var i = 0; i < lastDays; i++)
            {
                var date = DateTime.UtcNow.AddDays(i * -1);
                var ordersCount = this.dealerService.SumOfSalesForADay(this.User.GetId(), date);

                days.Add(date.ToString("dd/MM"));
                sales.Add(ordersCount);
            }

            var jsonModel = new
            {
                success = true,
                Days = days,
                Sales = sales,
            };

            return this.Json(jsonModel);
        }
    }
}
