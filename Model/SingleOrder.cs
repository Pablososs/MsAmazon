namespace BackgroundServiceAmazon.Model
{
    public class SingleOrder{

            public class Root
            {
                public Payload payload { get; set; }
            }

            public class BuyerCustomizedInfo
            {
                public string CustomizedURL { get; set; }
            }

            public class BuyerInfo
            {
                public BuyerCustomizedInfo BuyerCustomizedInfo { get; set; }
                public string GiftMessageText { get; set; }
                public GiftWrapPrice GiftWrapPrice { get; set; }
                public string GiftWrapLevel { get; set; }
            }

            public class BuyerRequestedCancel
            {
                public bool IsBuyerRequestedCancel { get; set; }
                public string BuyerCancelReason { get; set; }
            }

            public class CODFee
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

            public class CODFeeDiscount
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

            public class GiftWrapPrice
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

            public class ItemPrice
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

            public class OrderItemt
            {
                public string ASIN { get; set; }
                public string OrderItemId { get; set; }
                public string Title { get; set; }
                public int QuantityOrdered { get; set; }
                public int QuantityShipped { get; set; }
                public PointsGranted PointsGranted { get; set; }
                public ItemPrice ItemPrice { get; set; }
                public ShippingPrice ShippingPrice { get; set; }
                public DateTime ScheduledDeliveryEndDate { get; set; }
                public DateTime ScheduledDeliveryStartDate { get; set; }
                public CODFee CODFee { get; set; }
                public CODFeeDiscount CODFeeDiscount { get; set; }
                public string PriceDesignation { get; set; }
                public BuyerInfo BuyerInfo { get; set; }
                public BuyerRequestedCancel BuyerRequestedCancel { get; set; }
                public List<string> SerialNumbers { get; set; }
                public string AmazonOrderID { get; set; }
            }

            public class Payload
            {
                public string AmazonOrderId { get; set; }
                public string NextToken { get; set; }
                public List<OrderItemt> OrderItems { get; set; }
            }

            public class PointsGranted
            {
                public int PointsNumber { get; set; }
                public PointsMonetaryValue PointsMonetaryValue { get; set; }
            }

            public class PointsMonetaryValue
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

            public class ShippingPrice
            {
                public string CurrencyCode { get; set; }
                public string Amount { get; set; }
            }

    }
}
