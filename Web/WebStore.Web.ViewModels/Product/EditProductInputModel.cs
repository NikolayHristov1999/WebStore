namespace WebStore.Web.ViewModels.Product
{
    using AutoMapper;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class EditProductInputModel : ProductInputModel, IMapFrom<Product>, IHaveCustomMappings
    {
        public bool IsDeleted { get; set; }

        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
