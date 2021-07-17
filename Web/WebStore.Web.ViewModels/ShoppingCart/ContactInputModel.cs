namespace WebStore.Web.ViewModels.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ContactInputModel : IMapTo<Contact>
    {
        [Required]
        [RegularExpression(@"[а-зА-Зa-zA-Z\s]*")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"[а-зА-Зa-zA-Z\s]*")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "*Address")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[А-За-зa-zA-Z\s]*")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"[А-За-зa-zA-Z\s]*")]
        [Display(Name = "City")]
        public string City { get; set; }

        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$")]
        [Display(Name = "*Phone")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"[0-9]*")]
        [Display(Name = "*Zip")]
        public string Zip { get; set; }

    }
}
