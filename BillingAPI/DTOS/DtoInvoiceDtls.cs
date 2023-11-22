namespace BillingAPI.DTOS
{
    public class DtoInvoiceDtls
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
