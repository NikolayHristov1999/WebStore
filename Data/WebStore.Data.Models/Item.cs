using System;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class Item : BaseDeletableModel<string>
    {
        public Item()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string CartId { get; set; }

        public Cart Cart { get; set; }

        public bool IsPurchased { get; set; }
    }
}
