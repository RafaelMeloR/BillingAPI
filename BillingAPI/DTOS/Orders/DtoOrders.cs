namespace BillingAPI.DTOS.Orders
{
    public class DtoOrders
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public int userId { get; set; }
        public DateTime updatedDate { get; set; }
        public bool status { get; set; }
        public string accountNumber { get; set; }
        public int invoicePeriod { get; set; }
    }
}
