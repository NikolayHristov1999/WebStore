namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Cart : BaseDeletableModel<int>
    {
        public Cart()
        {
            this.Items = new HashSet<Item>();
        }

        public bool IsPurchased { get; set; }

        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
