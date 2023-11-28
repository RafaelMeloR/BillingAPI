using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public JobController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("/run")]
        public ActionResult<HttpResponse> run()
        { 
            Console.WriteLine("Job run");
            return Ok("Job run");
        }

    }
}
