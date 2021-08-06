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
        private readonly IDealerService dealerService;
        private readonly IContactService contactService;

        public DealersController(
            IUsersService usersService,
            IDealerService salesmanService,
            IContactService contactService)
        {
            this.usersService = usersService;
            this.dealerService = salesmanService;
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            var model = this.dealerService.GetAllDealers<TableDealerViewModel>();
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.dealerService.GetDealerByUserId<DetailsDealerViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            var dealerSales = this.dealerService.GetAllOrdersForSeller<TableOrderViewModel>(id)
               .OrderByDescending(x => x.CreatedOn)
               .ToList();

            model.DealersSales = dealerSales;

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

            if (!await this.dealerService.ChangeDealerStatusAsync(id, status))
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
