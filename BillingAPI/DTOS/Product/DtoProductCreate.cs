namespace BillingAPI.DTOS.Product
{
    public class DtoProductCreate
    {
        public string Name { get; set; }
        public decimal productUniquePrice { get; set; }
        public int serviceId { get; set; }
    }
}
