namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[а-зА-Зa-zA-Z\s]*")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[А-За-зa-zA-Z\s]*")]
        public string City { get; set; }

        public string Zip { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
