﻿using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public PaymentController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<Payment> GetPayment(int id)
        {
            if (id > 0)
            {
                var payment = _context.Payment.Find(id);
                DtoPayment dtoPayment = new DtoPayment();
                if (payment != null)
                {
                    dtoPayment.Id = payment.Id;
                    dtoPayment.InvoiceId = payment.InvoiceId;
                    dtoPayment.PaymentMethod = payment.PaymentMethod;
                    dtoPayment.PaymentDate = payment.PaymentDate;
                    dtoPayment.Amount = payment.Amount;
                    dtoPayment.PaymentStatus = payment.PaymentStatus;
                    return Ok(dtoPayment);
                }
                else
                {
                    return BadRequest("Payment not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }
    }
}
