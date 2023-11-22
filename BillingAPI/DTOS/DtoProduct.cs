namespace BillingAPI.DTOS
{
    public class DtoProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float productUniquePrice { get; set; }
        public int serviceId { get; set; }
    }
}
