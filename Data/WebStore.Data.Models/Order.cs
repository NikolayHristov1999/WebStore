namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
