namespace WebStore.Web.ViewModels.Categories
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class EditCategoryInputModel : IMapFrom<Category> 
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Parent Category")]
        public string CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
