using BillingAPI.DTOS;
using BillingAPI.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Net;
using System.Numerics;
using System.Xml.Linq;
using System;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public UserController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<User> GetUser(int id)
        {
            if (id > 0)
            {
                
                var user = _context.User.Find(id);
                DtoUser dtoUser = new DtoUser();
                if (user != null)
                {
                    dtoUser.id = user.id;
                    dtoUser.email = user.email;
                    dtoUser.firstName = user.firstName;
                    dtoUser.lastName = user.lastName;
                    dtoUser.addressId = user.addressId;
                    dtoUser.phone = user.phone;
                    dtoUser.ipAddress = user.ipAddress;
                    dtoUser.macAddress = user.macAddress;
                    dtoUser.lastLogin = user.lastLogin;
                    dtoUser.userType = user.usertype;
                    dtoUser.status = user.status;
                    return Ok(dtoUser);
                }
                else
                {
                    return BadRequest("User not found");
                }
               
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Users")]
        public ActionResult<User> GetUsers()
        {
            var users = _context.User.Select(user =>
            new
             {
                                user.id,
                                user.email,
                                user.firstName,
                                user.lastName,
                                user.addressId,
                                user.phone,
                                user.ipAddress,
                                user.macAddress,
                                user.lastLogin,
                                user.usertype,
                                user.status,
                            }).ToList();
            return Ok(users);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("login")]
        public ActionResult<User> Login([FromBody] User credentials)
        {
            var user = _context.User.Where(user => user.email.Equals(credentials.email) && user.password.Equals(credentials.password)).FirstOrDefault();
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("loginEmail")]
        public ActionResult<DtoUserFull> LoginUserPassword([FromHeader] string email, string password)
        {
            var user = _context.User.Where(user => user.email.Equals(email) && user.password.Equals(password)).FirstOrDefault();
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("loginEmailPass")]
        public ActionResult<DtoUserFull> LoginUserPasswordFull([FromBody] DtoUserLogin login)
        {

            var user = _context.User.Where(user => user.email.Equals(login.Email) && user.password.Equals(login.Password)).FirstOrDefault();
            DtoUserFull dtoUserFull = new DtoUserFull();
            Random random = new Random();
            if (user != null)
            {
                dtoUserFull.user = user;
                dtoUserFull.address = _context.Address.Where(address => address.id.Equals(user.addressId)).FirstOrDefault();

                var order = _context.Orders
                .Where(order => order.userId.Equals(user.id))
                .Select(order => order.Id)
                .FirstOrDefault();

                if (order == 0)
                {
                    if (_context.User.Find(user.id) != null)
                    {
                        _context.Database.ExecuteSqlRaw(
                            "EXEC [dbo].[CreateOrder]  @UserId, @ProductId, @accountNumber",
                            new SqlParameter("@UserId", user.id),
                            new SqlParameter("@ProductId", 1),
                            //new SqlParameter("@accountNumber", ("A" + random.Next(100, 1000)))
                            new SqlParameter("@accountNumber", ("A" + user.phone))
                            );
                    }
                    
                }
                
                
                    var ListInvoiceDtls = 

                    dtoUserFull.invoice = _context.Invoice.Where(Invoice => Invoice.OrderId.Equals(order)).Select(Invoice =>
                    new DtoInvoice
                    {
                       Id= Invoice.Id,
                       OrderId= Invoice.OrderId,
                       InvoiceDueDate= Invoice.InvoiceDueDate,
                       InvoiceTotal= Invoice.InvoiceTotal,
                       InvoiceDate=Invoice.InvoiceDate,
                       InvoicePeriod= Invoice.InvoicePeriod,
                       GST= Invoice.GST,
                       QST = Invoice.QST,
                       InvoiceNumber= Invoice.InvoiceNumber,
                       InvoiceStatus= Invoice.InvoiceStatus,
                       InvoiceAmount= Invoice.InvoiceAmount,
                       InvoiceDtls= _context.InvoiceDtls
                   .Where(InvoiceDtl => InvoiceDtl.InvoiceId.Equals(Invoice.Id))
                   .Select(InvoiceDtls =>
                    new DtoInvoiceDtls
                     {
                    Id = InvoiceDtls.Id,
                    InvoiceId = InvoiceDtls.InvoiceId,
                    ProductId = InvoiceDtls.ProductId,
                    ProductPrice = InvoiceDtls.ProductPrice,
                    }).ToList(),
                    }).ToList();
                    return Ok(dtoUserFull);
                 
            }
            else
            {
                return  BadRequest();
            }
             
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("create")]
        public ActionResult<DtoCreateUser> CreateUser([FromBody] DtoCreateUser Dtouser)
        {
            if (Dtouser != null)
            {

                _context.Database.ExecuteSqlRaw(
            "EXEC [dbo].[CreateUser]  @Password, @Email, @Name, @LastName, @Address, @City, @Province, @Country, @PostalCode, @Phone, @IpAddress, @MacAddress, @LastLogin, @UserType, @Status",
                new SqlParameter("@Password", Dtouser.password),
                new SqlParameter("@Email", Dtouser.email),
                new SqlParameter("@Name", Dtouser.firstName),
                new SqlParameter("@LastName", Dtouser.lastName),
                new SqlParameter("@Address", Dtouser.address),
                new SqlParameter("@City", Dtouser.city),
                new SqlParameter("@Province", Dtouser.province),
                new SqlParameter("@Country", Dtouser.country),
                new SqlParameter("@PostalCode", Dtouser.postalCode),
                new SqlParameter("@Phone", Dtouser.phone),
                new SqlParameter("@IpAddress", "10.0.0.1"),
                new SqlParameter("@MacAddress", "00-B0-D0-63-C2-26"),
                new SqlParameter("@LastLogin", DateTime.Now),
                new SqlParameter("@UserType", Dtouser.userType),
                new SqlParameter("@Status", 1)
        );
                return Ok(Dtouser);
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}
