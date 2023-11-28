namespace BillingAPI.DTOS
{
    public class DtoInvoiceFull
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
        public bool InvoiceStatus { get; set; }
        public decimal InvoiceAmount { get; set; }

        //INVOICES DETAILS
        public List<DtoInvoiceDtls> InvoiceDtls { get; set; }

        //User
        public Models.User User { get; set; }

        public Models.Address Address { get; set; }

        public string ErrorMessage { get; set; }
    }

   
}
