namespace WebStore.Web.ViewModels.ShoppingCart
{
    public abstract class ItemBaseModel
    {
        public string Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
