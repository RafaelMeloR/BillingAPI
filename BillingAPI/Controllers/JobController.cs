using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("/ManuallyRunJob")]
        public ActionResult run()
        {  
           // HttpClient client = new HttpClient();
            //var response = client.PostAsync("https://jobbillingapi4.azurewebsites.net/api/HttpTrigger1", null);

            _context.Database.ExecuteSqlRaw("EXECUTE [dbo].[JobGenerateInvoice] ");

            return Ok("Job executed");
        }

    }
}
