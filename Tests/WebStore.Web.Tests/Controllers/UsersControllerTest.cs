namespace WebStore.Web.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using WebStore.Data.Models;
    using WebStore.Web.Controllers;
    using WebStore.Web.ViewModels.Contact;
    using Xunit;

    public class UsersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<UsersController>
                .Instance(data =>
                    data.WithUser("TestUser"))
                .Calling(c => c.BecomeDealer())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("FirstName", "LastName", "+359888888888", "Kop str.", "TestAdress", "Sofia", "BUL")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string firstName,
            string lastName,
            string phoneNumber,
            string street,
            string address,
            string city,
            string country,
            string zip = "1000")
            => MyController<UsersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.BecomeDealer(new ContactDealerFormModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Street = street,
                    Address = address,
                    City = city,
                    Country = country,
                    Zip = zip,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Dealer>(dealers => dealers
                        .Any(d =>
                            d.Contact.FirstName == firstName &&
                            d.Contact.LastName == lastName &&
                            d.Contact.PhoneNumber == phoneNumber &&
                            d.Contact.Street == street &&
                            d.Contact.Address == address &&
                            d.Contact.City == city &&
                            d.UserId == TestUser.Identifier)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
