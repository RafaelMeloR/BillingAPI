using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public OrdersController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")] 
        public ActionResult<Orders> GetOrder(int id)
        {
            if (id > 0)
            {
                var orders = _context.Orders.Find(id);
                DtoOrders dtoOrders = new DtoOrders();
                if (orders != null)
                {
                    dtoOrders.Id = orders.Id;
                    dtoOrders.creationDate = orders.creationDate;
                    dtoOrders.userId = orders.userId;
                    dtoOrders.accountId = orders.accountId;
                    dtoOrders.updatedDate = orders.updatedDate;
                    dtoOrders.status = orders.status;
                    dtoOrders.accountNumber = orders.accountNumber;
                    return Ok(dtoOrders);
                }
                else
                {
                    return BadRequest("Orders not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Orders")]
        public ActionResult<Orders> GetOrders()
        {
            var orders = _context.Orders.Select(orders =>
                                     new
                                     {
                              orders.Id,
                              orders.creationDate,
                              orders.userId,
                              orders.accountId,
                              orders.updatedDate,
                              orders.status,
                              orders.accountNumber
                          }).ToList();
            return Ok(orders);
        }
    }
}
