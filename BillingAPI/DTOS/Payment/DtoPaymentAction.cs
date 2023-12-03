using System.Diagnostics.CodeAnalysis;

namespace BillingAPI.DTOS.Payment
{
    public class DtoPaymentAction
    {
        public int InvoiceId { get; set; } 
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
