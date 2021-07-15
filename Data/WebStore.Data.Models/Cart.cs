namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Cart : BaseDeletableModel<string>
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<Item>();
        }

        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Order Order { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
