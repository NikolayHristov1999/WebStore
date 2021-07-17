namespace WebStore.Web.ViewModels.Administration.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper.Configuration.Annotations;
    using WebStore.Services.Mapping;
    using WebStore.Data.Models;

    public class EditProductViewModel : IMapFrom<Product>, IMapTo<Product>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }


        [Display(Name = "Short Desciprtion")]
        public string ShortDescription { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "First Category")]
        [Ignore]
        public string FirstCategory { get; set; }

        [Display(Name = "Second Category")]
        [Ignore]
        public string SecondCategory { get; set; }

        [Display(Name = "Third Category")]
        [Ignore]
        public string ThirdCategory { get; set; }

        [Ignore]
        public IEnumerable<KeyValuePair<string, string>> AllCategories { get; set; }
    }
}
