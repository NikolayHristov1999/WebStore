namespace WebStore.Web.ViewModels.Administration.Orders
{
    public class ItemViewModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}