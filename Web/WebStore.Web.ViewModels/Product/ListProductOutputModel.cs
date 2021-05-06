namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ListProductOutputModel : BaseProductOutputModel, IMapFrom<Product>
    {
    }
}
