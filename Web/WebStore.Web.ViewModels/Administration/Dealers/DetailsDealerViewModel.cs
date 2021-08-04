namespace WebStore.Web.ViewModels.Administration.Dealers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Administration.Orders;
    using WebStore.Web.ViewModels.Contact;

    public class DetailsDealerViewModel : IMapFrom<Dealer>
    {
        public ContactViewModel Contact { get; set; }

        [Required]
        [Display(Name = "Dealer Status")]
        public string Status { get; set; }

        [Ignore]
        public ICollection<TableOrderViewModel> DealersSales { get; set; }

    }
}
