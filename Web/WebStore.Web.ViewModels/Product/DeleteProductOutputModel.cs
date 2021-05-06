namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class DeleteProductOutputModel : BaseProductOutputModel, IMapFrom<Product>
    {
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Added by User")]
        public string AddedByUserName { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Last Modified On")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
