namespace BillingAPI.Models
{
    public class InvoiceDtls
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }  
    }
}
