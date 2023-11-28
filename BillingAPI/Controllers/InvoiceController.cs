using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<DtoInvoiceFull> GetInvoice(int id)
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

                DtoInvoiceFull dtoInvoice = new DtoInvoiceFull();
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
                    var UserId = _context.Orders
                        .Where(order => order.Id.Equals(dtoInvoice.OrderId))
                        .Select(order => order.Id)
                        .FirstOrDefault();
                    DtoUser dtoUser = new DtoUser();  
                    dtoInvoice.User = _context.User.Find(UserId);
                    dtoInvoice.Address = _context.Address.Find(dtoInvoice.User.addressId);
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


        [HttpGet]
        [ApiVersion("1.0")]
        [Route("UserInvoices")]
        public ActionResult<DtoInvoice> GetInvoicesByUser(int UserId)
        {
            var order = _context.Orders
                .Where(order => order.userId.Equals(UserId))
                .Select(order => order.Id)
                .FirstOrDefault();

            if (order == 0)
            {
                if (_context.User.Find(UserId)!=null)
                {
                    return BadRequest("User has no Invoices");
                }
                else
                {
                    return BadRequest("User not found");
                }
               
            }
            else
            {
                var Invoices = _context.Invoice.Where(Invoice => Invoice.OrderId.Equals(order)).Select(Invoice =>
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
}
