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
    using WebStore.Web.ViewModels.Reviews;

    public class SingleProductViewModel : BaseProductViewModel, IMapFrom<Product>, IHaveCustomMappings
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

        [MinLength(3)]
        [Required]
        [Display(Name = "Your Full Name")]
        public string FullName { get; set; }

        [MinLength(5)]
        [Required]
        [Display(Name = "Review Content")]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Stars { get; set; }

        public int AvailableQuantity { get; set; }

        public ListReviewsViewModel ProductReviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductViewModel>()
                .ForMember(x => x.AddedByUserName, opt => opt.MapFrom(x => x.AddedByUser.UserName ?? x.AddedByUser.Email))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Categories.FirstOrDefault().Category.Name));
        }
    }
}
