namespace WebStore.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ContactFormModel : IMapTo<Contact>
    {
        [Required]
        [RegularExpression(@"[а-зА-Зa-zA-Z\s]*")]
        [Display(Name = "First Name")]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"[а-зА-Зa-zA-Z\s]*")]
        [Display(Name = "Last Name")]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Street")]
        [MaxLength(200)]
        public string Street { get; set; }

        [Display(Name = "*Address")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[А-За-зa-zA-Z\s]*")]
        [Display(Name = "Country")]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"[А-За-зa-zA-Z\s]*")]
        [Display(Name = "City")]
        [MaxLength(100)]
        public string City { get; set; }

        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$")]
        [Display(Name = "Phone")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"[0-9]*")]
        [Display(Name = "*Zip")]
        [MaxLength(5)]
        public string Zip { get; set; }
    }
}
