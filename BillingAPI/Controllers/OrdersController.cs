using BillingAPI.DTOS.Orders;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                    dtoOrders.updatedDate = orders.updatedDate;
                    dtoOrders.status = orders.status;
                    dtoOrders.accountNumber = orders.accountNumber;
                    dtoOrders.invoicePeriod = orders.invoicePeriod;
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
        public ActionResult<DtoOrders> GetOrders()
        {
            var orders = _context.Orders.Select(orders =>
                                     new
                                     {
                              orders.Id,
                              orders.creationDate,
                              orders.userId,
                              orders.updatedDate,
                              orders.status,
                              orders.accountNumber,
                              orders.invoicePeriod
                          }).ToList();
            return Ok(orders);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("CreateOrder")]
        public ActionResult<Orders> createOrder([FromBody] DtoOrdersCreate order)
        {
            _context.Database.ExecuteSqlRaw("EXECUTE [dbo].[CreateOrder]  @userId,@ProductId,@accountNumber",
               new SqlParameter("@userId", order.userId),
               new SqlParameter("@ProductId", order.ProductId),
               new SqlParameter("@accountNumber", order.accountNumber));
            Orders orders = new Orders();
            orders = _context.Orders.Where(x => x.userId == order.userId).FirstOrDefault(); 
            return orders;
        }
    }
}
