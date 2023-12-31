﻿using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

 
namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AddressController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<Address> GetAddress(int id)
        {
            if (id > 0)
            {

                var address = _context.Address.Find(id);
                DtoAddress dtoAddress = new DtoAddress();
                if (address != null)
                {
                    dtoAddress.id = address.id;
                    dtoAddress.address = address.address;
                    dtoAddress.city = address.city;
                    dtoAddress.province = address.province;
                    dtoAddress.country = address.country;
                    dtoAddress.postalCode = address.postalCode;
                    dtoAddress.status = address.status;
                    return Ok(dtoAddress);
                }
                else
                {
                    return BadRequest("Address not found");
                }

            }
            return BadRequest("Id must be greater than 0");
        }


        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Addresses")]
        public ActionResult<Address> GetAddresses()
        {
            var Addresses = _context.Address.Where(Address => Address.status.Equals(true)).Select(Address =>
                   new
                   {
                       Address.id,
                       Address.address,
                       Address.city,
                       Address.province,
                       Address.country,
                       Address.postalCode,
                       Address.status
                   }).ToList();

            return Ok(Addresses);
        }
    }
}
