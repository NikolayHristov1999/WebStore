namespace WebStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WebStore.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        public byte Stars { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
