﻿using BillingAPI.DTOS;
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
        public ActionResult<Orders> GetOrders(int id)
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
    }
}