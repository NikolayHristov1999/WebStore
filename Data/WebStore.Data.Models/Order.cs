namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebStore.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
