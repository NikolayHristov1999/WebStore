namespace WebStore.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Contact;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult BecomeDealer()
        {
            if (this.usersService.IsDealer(this.User.GetId()))
            {
                this.TempData["Message"] = "We have your dealer submission already.";
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> BecomeDealer(ContactDealerFormModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            model.Email = user.Email;

            if (this.usersService.IsDealer(user.Id))
            {
                this.TempData["Message"] = "We have your dealer submission already.";
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.usersService.BecomeDealerAsync(this.User.GetId(), model);

            this.TempData["Message"] = "We recieved your request successfuly. We need time to process it.";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
