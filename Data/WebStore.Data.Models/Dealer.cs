namespace WebStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WebStore.Data.Common.Models;

    public class Dealer : BaseDeletableModel<string>
    {
        public Dealer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
