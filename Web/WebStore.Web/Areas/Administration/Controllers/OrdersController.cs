namespace WebStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Orders;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly ISalesmanService salesmanService;
        private readonly IContactService contactService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(
            IOrdersService ordersService,
            ISalesmanService salesmanService,
            IContactService contactService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.salesmanService = salesmanService;
            this.contactService = contactService;
            this.productsService = productsService;
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

            return this.View(tableOrderModel);
        }


        public async Task<IActionResult> ById(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var order = this.salesmanService.GetOrderForSellerById(id);

            if (!(this.User.IsInRole("Administrator") || user.Id == order.SellerId))
            {
                return this.Unauthorized();
            }

            var contact = this.contactService.GetContactById(order.ContactId);
            var items = this.salesmanService.GetAllItemsBySellerOrderId(id);
            var itemsModel = new List<ItemViewModel>();

            foreach (var item in items)
            {
                itemsModel.Add(new ItemViewModel
                {
                    TotalPrice = item.ItemTotalPrice,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    ProductName = this.productsService.GetProductById(item.ProductId).Name,
                });
            }

            var model = new OrderViewModel
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice.ToString("f2"),
                PaymentMethod = order.PaymentMethod,
                ShippingMethod = order.ShippingMethod,
                CreatedOn = order.CreatedOn.ToString(),
                ContactInfo = new ContactViewModel
                {
                    ClientName = contact.FirstName + " " + contact.LastName,
                    Email = contact.Email,
                    ShippingStreet = contact.Street,
                    City = contact.City,
                    Country = contact.Country,
                    Zip = contact.Zip,
                    Address = contact.Address,
                },
                Items = itemsModel,
            };

            return this.View(model);
        }

        public async Task<IActionResult> LastTwoWeekSales()
        {
            var days = new List<string>();
            var sales = new List<int>();
            var user = await this.userManager.GetUserAsync(this.User);
            var orders = this.salesmanService.GetAllOrdersForSeller(user.Id);

            for (var i = 0; i < 14; i++)
            {
                var date = DateTime.UtcNow.AddDays(i * -1);
                var ordersCount = orders
                    .Where(x => x.CreatedOn.Day == date.Day &&
                    x.CreatedOn.Month == date.Month &&
                    x.CreatedOn.Year == date.Year)
                    .Count();

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
