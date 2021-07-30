namespace WebStore.Data.Common
{
    public class DataConstants
    {
        public enum DealerStatus
        {
            Pending,
            Approved,
            Rejected,
            Disabled,
        }

        public enum OrdersStatus
        {
            Created,
            ForReview,
            Finished,
            Send,
            Accepted,
            Denied,
        }

        public enum PaymentMethod
        {
            Visa = 1,
            Paypal = 2,
            OnShip = 3,
        }
    }
}
