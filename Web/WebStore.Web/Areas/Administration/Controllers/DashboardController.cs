namespace WebStore.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Dashboard;
    using WebStore.Web.ViewModels.Administration.Orders;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ISalesmanService salesmanService;
        private readonly IContactService contactService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            ISettingsService settingsService,
            ISalesmanService salesmanService,
            IContactService contactService,
            UserManager<ApplicationUser> userManager)
        {
            this.settingsService = settingsService;
            this.salesmanService = salesmanService;
            this.contactService = contactService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var orders = this.salesmanService.GetAllOrdersForSeller(user.Id)
                .OrderByDescending(x => x.CreatedOn);
            var tableOrderModel = new List<TableOrderViewModel>();

            foreach (var order in orders)
            {
                var contact = this.contactService.GetContactById(order.ContactId);
                tableOrderModel.Add(new TableOrderViewModel
                {
                    SellerOrderId = order.Id,
                    Name = contact.FirstName + " " + contact.LastName,
                    Price = order.TotalPrice.ToString("f2"),
                    Email = contact.Email,
                    CreatedOn = order.CreatedOn,
                });
            }

            var viewModel = new IndexViewModel
            {
                SettingsCount = this.settingsService.GetCount(),
                TotalOrders = orders.Count(),
                TotalSalesUsd = orders.Sum(x => x.TotalPrice),
                TotalCustomers = orders.Where(x => x.BuyerId != null)
                            .GroupBy(x => x.BuyerId).Select(g => g.First()).Count() + orders
                            .Where(x => x.BuyerId == null).Count(),
                Orders = tableOrderModel,
            };

            return this.View(viewModel);
        }

        public IActionResult Charts()
        {
            return this.View();
        }
    }
}
