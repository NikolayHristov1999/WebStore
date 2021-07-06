
namespace WebStore.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class BaseCategoryViewModel : IMapFrom<Category>
    {

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
