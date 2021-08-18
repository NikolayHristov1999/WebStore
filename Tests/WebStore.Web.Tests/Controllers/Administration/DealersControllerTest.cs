namespace WebStore.Web.Tests.Controllers.Administration
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using WebStore.Web.Areas.Administration.Controllers;
    using WebStore.Web.ViewModels.Administration.Dealers;
    using Xunit;

    using static WebStore.Web.Tests.Data.Dealers;

    public class DealersControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewModelWithDealers()
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetFiveDealers()))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<TableDealerViewModel>>());

        [Fact]
        public void DetailsByDealerIdShouldReturnNotFoundWhenNoDealerExists()
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Details("0"))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void PendingShouldReturnViewModelWithDealers()
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetFiveDealers()))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<TableDealerViewModel>>());
    }
}
