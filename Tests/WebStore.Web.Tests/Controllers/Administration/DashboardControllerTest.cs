namespace WebStore.Web.Tests.Controllers.Administration
{

    using MyTested.AspNetCore.Mvc;
    using WebStore.Web.Areas.Administration.Controllers;
    using WebStore.Web.ViewModels.Administration.Dashboard;
    using Xunit;

    public class DashboardControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithModel()
            => MyController<DashboardController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>());

        [Fact]
        public void ChartsShouldReturnView()
            => MyController<DashboardController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Charts())
                .ShouldReturn()
                .View();

    }
}
