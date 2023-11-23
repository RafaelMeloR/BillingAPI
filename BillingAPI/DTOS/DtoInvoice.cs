namespace BillingAPI.DTOS
{
    public class DtoInvoice
    { 
       public int Id { get; set; }
       public int OrderId { get; set; }
       public DateTime InvoiceDueDate { get; set; }
       public decimal InvoiceTotal { get; set; }
       public DateTime InvoiceDate { get; set; }
       public int InvoicePeriod { get; set; }
       public decimal GST { get; set; }
       public decimal QST { get; set; }
       public string InvoiceNumber { get; set; }
       public string InvoiceStatus { get; set; }
        public decimal InvoiceAmount { get; set; }
          
        public class InvoiceDtls
        {
            public int Id { get; set; }
            public int InvoiceId { get; set; }
            public int ProductId { get; set; }
            public decimal ProductPrice { get; set; }
        }
    }
}
