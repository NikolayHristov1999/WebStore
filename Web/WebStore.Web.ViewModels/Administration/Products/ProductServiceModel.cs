namespace WebStore.Web.ViewModels.Administration.Products
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ProductServiceModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }

        public string AddedByUserId { get; set; }

        public long Views { get; set; }

        public string MadeIn { get; set; }

        public string StoredInCountry { get; set; }

        public bool Approved { get; set; }
    }
}
