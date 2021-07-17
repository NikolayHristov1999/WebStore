namespace WebStore.Web.ViewModels.Administration.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Services.Mapping;
    using WebStore.Data.Models;

    public class TableProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
