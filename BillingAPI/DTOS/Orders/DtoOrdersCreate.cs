namespace BillingAPI.DTOS.Orders
{
    public class DtoOrdersCreate
    {
        public int userId { get; set; } 
        public string accountNumber { get; set; }   
        public int ProductId { get; set; } 
    }
}
