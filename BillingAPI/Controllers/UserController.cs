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
                    dtoUser.userName = user.userName;
                    dtoUser.email = user.email;
                    dtoUser.name = user.name;
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
                                user.userName,
                                user.email,
                                user.name,
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
            var user = _context.User.Where(user => user.userName.Equals(credentials.userName) && user.password.Equals(credentials.password)).FirstOrDefault();
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
        public ActionResult<User> LoginUserPassword([FromHeader] string email, string password)
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
        [Route("create")]
        public ActionResult<DtoCreateUser> CreateUser([FromBody] DtoCreateUser Dtouser)
        {
            if (Dtouser != null)
            {

                _context.Database.ExecuteSqlRaw(
            "EXEC [dbo].[CreateUser] @UserName, @Password, @Email, @Name, @LastName, @Address, @City, @Province, @Country, @PostalCode, @Phone, @IpAddress, @MacAddress, @LastLogin, @UserType, @Status",
                new SqlParameter("@UserName", Dtouser.userName),
                new SqlParameter("@Password", Dtouser.password),
                new SqlParameter("@Email", Dtouser.email),
                new SqlParameter("@Name", Dtouser.name),
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
