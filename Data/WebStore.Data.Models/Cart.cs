using System;
using System.Collections.Generic;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class Cart : BaseDeletableModel<string>
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
