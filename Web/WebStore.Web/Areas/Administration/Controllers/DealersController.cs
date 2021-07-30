namespace WebStore.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Common;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Dealers;
    using WebStore.Web.ViewModels.Administration.Orders;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DealersController : AdministrationController
    {
        private readonly IUsersService usersService;
        private readonly ISalesmanService salesmanService;
        private readonly IContactService contactService;

        public DealersController(
            IUsersService usersService,
            ISalesmanService salesmanService,
            IContactService contactService)
        {
            this.usersService = usersService;
            this.salesmanService = salesmanService;
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.salesmanService.GetDealerByUserId<DetailsDealerViewModel>(id);
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

        public IActionResult Pending()
        {
            var dealersModel = this.usersService.GetPendingDealers<PendingDealerViewModel>();
            return this.View(dealersModel);
        }
    }
}
