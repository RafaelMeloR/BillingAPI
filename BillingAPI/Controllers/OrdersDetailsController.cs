using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDetailsController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        
        public OrdersDetailsController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [Route("OrdersDetail")]
        public ActionResult<OrdersDetails> GetOrdersDetail(int id)
        {
            if (id > 0)
            {
                var ordersDetails = _context.OrdersDetails.Find(id);
                DtoOrdersDetails dtoOrdersDetails = new DtoOrdersDetails();
                if (ordersDetails != null)
                {
                    dtoOrdersDetails.Id = ordersDetails.Id;
                    dtoOrdersDetails.OrderId = ordersDetails.OrderId;
                    dtoOrdersDetails.ProductId = ordersDetails.ProductId;
                    return Ok(dtoOrdersDetails);
                }
                else
                {
                    return BadRequest("OrdersDetails not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [Route("OrdersDetails")]
        public ActionResult GetOrdersDetails(int id)
        {
            var ordersDetails = _context.OrdersDetails.Where(ordersDetails => ordersDetails.OrderId.Equals(id)).Select(ordersDetails =>
                                     new
                                     {
                       ordersDetails.Id,
                       ordersDetails.OrderId,
                       ordersDetails.ProductId,
                   }).ToList();
            return Ok(ordersDetails);
        }
    }
}
