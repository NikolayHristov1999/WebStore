namespace WebStore.Web.Areas.Administration.Controllers
{
    using WebStore.Common;
    using WebStore.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.DealerRoleName)]
    [Area("Administration")]

    public class AdministrationController : BaseController
    {
    }
}
