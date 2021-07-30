namespace WebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Contact;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
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
        public async Task<IActionResult> BecomeDealer(ContactFormModel model)
        {
            if (this.usersService.IsDealer(this.User.GetId()))
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
