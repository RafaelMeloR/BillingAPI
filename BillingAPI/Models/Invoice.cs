namespace BillingAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public decimal InvoiceTotal { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoicePeriod { get; set; }
        public decimal GST { get; set; }
        public decimal QST { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceStatus { get; set; } 
        public decimal InvoiceAmount { get; set; } 
        
    }
}
