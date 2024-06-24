namespace BackgroundServiceAmazon.Model
{
    public class Ordersapi
    {
        public class BuyerInfo
        {
            public string BuyerEmail { get; set; }
            public string BuyerName { get; set; }
            public BuyerTaxInfo BuyerTaxInfo { get; set; }
            public string PurchaseOrderNumber { get; set; }
        }

        public class BuyerTaxInfo
        {
            public string CompanyLegalName { get; set; }
        }

        public class Ordert
        {
            public string AmazonOrderId { get; set; }
            public DateTime PurchaseDate { get; set; }
            public DateTime LastUpdateDate { get; set; }
            public string OrderStatus { get; set; }
            public string FulfillmentChannel { get; set; }
            public int NumberOfItemsShipped { get; set; }
            public int NumberOfItemsUnshipped { get; set; }
            public string PaymentMethod { get; set; }
            public List<string> PaymentMethodDetails { get; set; }
            public string MarketplaceId { get; set; }
            public string ShipmentServiceLevelCategory { get; set; }
            public string OrderType { get; set; }
            public DateTime EarliestShipDate { get; set; }
            public DateTime LatestShipDate { get; set; }
            public bool IsBusinessOrder { get; set; }
            public bool IsPrime { get; set; }
            public bool IsAccessPointOrder { get; set; }
            public bool IsGlobalExpressEnabled { get; set; }
            public bool IsPremiumOrder { get; set; }
            public bool IsSoldByAB { get; set; }
            public bool IsIBA { get; set; }
            public ShippingAddress ShippingAddress { get; set; }
            public BuyerInfo BuyerInfo { get; set; }
        }

        public class Payload
        {
            public string NextToken { get; set; }
            public List<Ordert> Orders { get; set; }
        }

        public class Root
        {
            public Payload payload { get; set; }
        }

        public class ShippingAddress
        {
            public string Name { get; set; }
            public string AddressLine1 { get; set; }
            public string City { get; set; }
            public string StateOrRegion { get; set; }
            public string PostalCode { get; set; }
            public string CountryCode { get; set; }
        }
    }
}
