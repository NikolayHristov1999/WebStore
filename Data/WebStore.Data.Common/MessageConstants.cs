namespace WebStore.Data.Common
{
    public class MessageConstants
    {
        public enum PaymentMethod
        {
            SuccessfulMessage,
            WarningMessage,
            ErrorMessage,
            InformationMessage,
        }

        public class SuccessfulMessages
        {
            public const string DealerRequestRecieved = "We recieved your request successfuly. We need time to process it.";

            public const string ProductAdded = "Product added successfully.";
        }

        public class InformationMessages
        {
            public const string DealerMessageAlreadyRecieved = "We have your dealer submission already.";
        }
    }
}
