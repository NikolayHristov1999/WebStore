namespace WebStore.Web.ViewModels.Product
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ListProductOutputModel : BaseProductOutputModel, IMapFrom<Product>, IHaveCustomMappings
    {

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ListProductOutputModel>()
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name.Length > GlobalConstants.MaximumLengthNameInView ?
                    x.Name.Substring(0, GlobalConstants.MaximumLengthNameInView) + "..." : 
                    x.Name));
        }
    }
}
