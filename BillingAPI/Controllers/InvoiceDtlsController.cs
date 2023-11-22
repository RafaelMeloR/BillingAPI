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
        public ActionResult<InvoiceDtls> GetInvoiceDtls(int id)
        {
            if (id > 0)
            {
                var invoiceDtls = _context.InvoiceDtls.Find(id);
                DtoInvoiceDtls dtoInvoiceDtls = new DtoInvoiceDtls();
                if (invoiceDtls != null)
                {
                    dtoInvoiceDtls.Id = invoiceDtls.Id;
                    dtoInvoiceDtls.InvoiceId = invoiceDtls.InvoiceId;
                    dtoInvoiceDtls.ProductId = invoiceDtls.ProductId;
                    dtoInvoiceDtls.ProductPrice = invoiceDtls.ProductPrice;
                    return Ok(dtoInvoiceDtls);
                }
                else
                {
                    return BadRequest("InvoiceDtls not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        } 
    }
}
