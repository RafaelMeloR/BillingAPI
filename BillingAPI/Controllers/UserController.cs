using BillingAPI.DTOS;
using BillingAPI.Models; 
using Microsoft.AspNetCore.Mvc;

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
                    dtoUser.password = user.password;
                    dtoUser.email = user.email;
                    dtoUser.name = user.name;
                    dtoUser.lastName = user.lastName;
                    dtoUser.addressId = user.addressId;
                    dtoUser.phone = user.phone;
                    dtoUser.ipAddress = user.ipAddress;
                    dtoUser.macAddress = user.macAddress;
                    dtoUser.lastLogin = user.lastLogin;
                    dtoUser.usertype = user.usertype;
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
        public ActionResult GetUsers()
        {
            var users = _context.User.Select(user =>
                                                            new
                                                            {
                                user.id,
                                user.userName,
                                user.password,
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
    }
}
