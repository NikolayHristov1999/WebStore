namespace WebStore.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Administration.Dashboard;
    using WebStore.Web.ViewModels.Administration.Orders;

    public class DashboardController : AdministrationController
    {
        private readonly IDealerService dealerService;

        public DashboardController(
            IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            var orders = this.dealerService.GetAllOrdersForSeller<TableOrderViewModel>(userId)
                .OrderByDescending(x => x.CreatedOn);

            var viewModel = new IndexViewModel
            {
                TotalSalesUsd = orders.Sum(x => x.TotalPrice),
                AverageRatingFromUsers = this.dealerService.GetAverageRatingFromUsers(userId).ToString("f1"),
                TotalCustomers = orders.Where(x => x.BuyerId != null)
                            .GroupBy(x => x.BuyerId).Select(g => g.First()).Count() + orders
                            .Where(x => x.BuyerId == null).Count(),
                Orders = orders,
            };

            return this.View(viewModel);
        }

        public IActionResult Charts()
        {
            return this.View();
        }
    }
}
