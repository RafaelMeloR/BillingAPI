using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDtlsController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public InvoiceDtlsController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<InvoiceDtls> GetInvoiceDtl(int id)
        {
            if (id > 0)
            {
                var invoiceDtl = _context.InvoiceDtls.Find(id);
                DtoInvoiceDtls dtoInvoiceDtl = new DtoInvoiceDtls();
                if (invoiceDtl != null)
                {
                    dtoInvoiceDtl.Id = invoiceDtl.Id;
                    dtoInvoiceDtl.InvoiceId = invoiceDtl.InvoiceId;
                    dtoInvoiceDtl.ProductId = invoiceDtl.ProductId;
                    dtoInvoiceDtl.ProductPrice = invoiceDtl.ProductPrice;
                    return Ok(dtoInvoiceDtl);
                }
                else
                {
                    return BadRequest("InvoiceDtls not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }


        [HttpGet]
        [ApiVersion("1.0")]
        [Route("InvoiceDtls")]
        public ActionResult GetInvoiceDtls(int id)
        {
            var InvoiceDtls = _context.InvoiceDtls.Where(InvoiceDtls => InvoiceDtls.InvoiceId.Equals(id)).Select(InvoiceDtls =>
                          new
                          {
                       InvoiceDtls.Id,
                       InvoiceDtls.InvoiceId,
                       InvoiceDtls.ProductId,
                       InvoiceDtls.ProductPrice, 
                   }).ToList();
            return Ok(InvoiceDtls);

        }
    }
}
