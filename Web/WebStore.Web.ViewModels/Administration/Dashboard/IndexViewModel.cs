namespace WebStore.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.Linq;

    using WebStore.Web.ViewModels.Administration.Orders;

    public class IndexViewModel
    {
        public decimal TotalSalesUsd { get; set; }

        public int TotalCustomers { get; set; }

        public string AverageRatingFromUsers { get; set; }

        public int TotalOrders => this.Orders.Count();

        public IEnumerable<TableOrderViewModel> Orders { get; set; }
    }
}
