namespace WebStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Dealers;
    using WebStore.Web.ViewModels.Administration.Orders;

    using static WebStore.Data.Common.DataConstants;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DealersController : AdministrationController
    {
        private readonly IUsersService usersService;
        private readonly IDealerService salesmanService;
        private readonly IContactService contactService;

        public DealersController(
            IUsersService usersService,
            IDealerService salesmanService,
            IContactService contactService)
        {
            this.usersService = usersService;
            this.salesmanService = salesmanService;
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            var model = this.salesmanService.GetAllDealers<TableDealerViewModel>();
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.salesmanService.GetDealerByUserId<DetailsDealerViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            var dealerSales = this.salesmanService.GetAllOrdersForSeller(id)
                .OrderByDescending(x => x.CreatedOn);
            var tableOrderModel = new List<TableOrderViewModel>();

            foreach (var order in dealerSales)
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

            model.DealersSales = tableOrderModel;

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Details(string id, string status)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!await this.salesmanService.ChangeDealerStatusAsync(id, status))
            {
                this.TempData["Message"] = "Unavailable user or dealer status!";
            }

            return this.RedirectToAction("Details");
        }

        public IActionResult Pending()
        {
            var dealersModel = this.usersService.GetPendingDealers<TableDealerViewModel>();
            return this.View(dealersModel);
        }
    }
}
