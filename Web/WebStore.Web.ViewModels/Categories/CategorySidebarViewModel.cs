namespace WebStore.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper.Configuration.Annotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategorySidebarViewModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        [Ignore]
        public ICollection<BaseCategoryViewModel> SubCategories { get; set; }
    }
}
