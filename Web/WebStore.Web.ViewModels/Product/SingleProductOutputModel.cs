namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Ganss.XSS;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class SingleProductOutputModel : BaseProductOutputModel, IMapFrom<Product>, IHaveCustomMappings
    {
        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }

        public string SanitizedShortDescription => new HtmlSanitizer().Sanitize(this.ShortDescription);

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        [Display(Name = "Added by User")]
        public string AddedByUserName { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductOutputModel>()
                .ForMember(x => x.AddedByUserName, opt => opt.MapFrom(x => x.AddedByUser.UserName ?? x.AddedByUser.Email))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Categories.FirstOrDefault().Category.Name));
        }
    }
}
