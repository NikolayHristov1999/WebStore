namespace WebStore.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseProductOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public string Category { get; set; }
    }
}
