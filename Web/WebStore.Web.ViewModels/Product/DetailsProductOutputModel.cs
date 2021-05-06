namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class DetailsProductOutputModel : BaseProductOutputModel, IMapFrom<Product>, IHaveCustomMappings
    {
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Added by User")]
        public string AddedByUserName { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Last Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, DetailsProductOutputModel>()
                .ForMember(x => x.AddedByUserName, opt => opt.MapFrom(x => x.AddedByUser.UserName ?? x.AddedByUser.Email))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Categories.FirstOrDefault().Category.Name));
        }
    }
}
