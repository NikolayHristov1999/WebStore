namespace WebStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Common.Models;

    public class Vote
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public byte Value { get; set; }
    }
}
