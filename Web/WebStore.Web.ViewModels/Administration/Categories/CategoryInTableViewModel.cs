namespace WebStore.Web.ViewModels.Administration.Categories
{
    using System;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryInTableViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public int ProductsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryInTableViewModel>()
                .ForMember(x => x.ParentCategoryName, opt => opt
                    .MapFrom(y => y.ParentCategory != null ? y.ParentCategory.Name : "None"));
        }
    }
}
