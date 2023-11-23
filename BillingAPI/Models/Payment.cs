namespace BillingAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; } // FK
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
