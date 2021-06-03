namespace WebStore.Web.ViewModels.Product
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ListProductOutputModel : BaseProductOutputModel, IMapFrom<Product>, IHaveCustomMappings
    {
        private const int LengthName = 40;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ListProductOutputModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.Length > LengthName ?
                    x.Name.Substring(0, LengthName) + "..." : x.Name));
        }
    }
}
