using BillingAPI.DTOS;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public ServiceController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<Service> GetService(int id)
        {
            if (id > 0)
            {
                var service = _context.Service.Find(id);
                DtoService dtoService = new DtoService();
                if (service != null)
                {
                    dtoService.id = service.id;
                    dtoService.name = service.name;
                    return Ok(dtoService);
                }
                else
                {
                    return BadRequest("Service not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Services")]
        public ActionResult GetServices()
        {
            var services = _context.Service.Select(service =>
                                                new
                                                {
                               service.id,
                               service.name,
                           }).ToList();
            return Ok(services);
        }
    }
}
