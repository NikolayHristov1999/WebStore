namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using WebStore.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Street { get; set; }

        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public string Zip { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
