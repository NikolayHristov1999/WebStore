namespace WebStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using WebStore.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(5)]
        public string Zip { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
