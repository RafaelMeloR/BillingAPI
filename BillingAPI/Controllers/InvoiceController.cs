using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public InvoiceController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<DtoInvoice> GetInvoice(int id)
        {
            if (id > 0)
            {
                var invoice = _context.Invoice.Find(id);
                var ListInvoiceDtls = _context.InvoiceDtls
                    .Where(InvoiceDtl => InvoiceDtl.InvoiceId.Equals(id))
                    .Select(InvoiceDtls =>
                 new DtoInvoiceDtls
                   {
                     Id=InvoiceDtls.Id,
                     InvoiceId= InvoiceDtls.InvoiceId,
                     ProductId = InvoiceDtls.ProductId,
                     ProductPrice=InvoiceDtls.ProductPrice, 
                   }).ToList();

                DtoInvoice dtoInvoice = new DtoInvoice();
                if (invoice != null)
                {
                    dtoInvoice.Id = invoice.Id;
                    dtoInvoice.OrderId = invoice.OrderId;
                    dtoInvoice.InvoiceDueDate = invoice.InvoiceDueDate;
                    dtoInvoice.InvoiceTotal = invoice.InvoiceTotal;
                    dtoInvoice.InvoiceDate = invoice.InvoiceDate;
                    dtoInvoice.InvoicePeriod = invoice.InvoicePeriod;
                    dtoInvoice.GST = invoice.GST;
                    dtoInvoice.QST = invoice.QST;
                    dtoInvoice.InvoiceNumber = invoice.InvoiceNumber;
                    dtoInvoice.InvoiceStatus = invoice.InvoiceStatus;
                    dtoInvoice.InvoiceAmount = invoice.InvoiceAmount;
                    dtoInvoice.InvoiceDtls = ListInvoiceDtls;
                    return Ok(dtoInvoice);
                }
                else
                {
                    return BadRequest("Invoice not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Invoices")]
        public ActionResult<Invoice> GetInvoices()
        {
            var Invoices = _context.Invoice.Where(Invoice => Invoice.InvoiceStatus.Equals(true)).Select(Invoice =>
            new
            { 
                Invoice.Id,
                Invoice.OrderId,
                Invoice.InvoiceDueDate,
                Invoice.InvoiceTotal,
                Invoice.InvoiceDate,
                Invoice.InvoicePeriod,
                Invoice.GST,
                Invoice.QST,
                Invoice.InvoiceNumber,
                Invoice.InvoiceStatus,
                Invoice.InvoiceAmount
                }).ToList();
            return Ok(Invoices);
        }
    }
}
