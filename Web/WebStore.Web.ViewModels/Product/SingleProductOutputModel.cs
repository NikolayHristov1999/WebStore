namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class SingleProductOutputModel : BaseProductOutputModel, IMapFrom<Product>, IHaveCustomMappings
    {
        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Added by User")]
        public string AddedByUserName { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, DetailsProductOutputModel>()
                .ForMember(x => x.AddedByUserName, opt => opt.MapFrom(x => x.AddedByUser.UserName ?? x.AddedByUser.Email))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Categories.FirstOrDefault().Category.Name));
        }
    }
}
