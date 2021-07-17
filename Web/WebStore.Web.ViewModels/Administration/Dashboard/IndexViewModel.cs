
namespace WebStore.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using WebStore.Web.ViewModels.Administration.Orders;

    public class IndexViewModel
    {
        public int SettingsCount { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalSalesUsd { get; set; }

        public int TotalCustomers { get; set; }

        public IEnumerable<TableOrderViewModel> Orders { get; set; }

    }
}
