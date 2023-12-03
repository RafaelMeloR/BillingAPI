using BillingAPI.DTOS.Product;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public ProductController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public ActionResult<Product> GetProduct(int id)
        {
            if (id > 0)
            {
                var product = _context.Product.Find(id);
                DtoProduct dtoProduct = new DtoProduct();
                if (product != null)
                {
                    dtoProduct.Id = product.Id;
                    dtoProduct.Name = product.Name;
                    dtoProduct.productUniquePrice = product.productUniquePrice;
                    dtoProduct.serviceId = product.serviceId;
                    return Ok(dtoProduct);
                }
                else
                {
                    return BadRequest("Product not found");
                }
            }
            return BadRequest("Id must be greater than 0");
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Products")]
        public ActionResult<Product> GetProducts()
        {
            var products = _context.Product.Select(product =>
                                                new
                                                {
                               product.Id,
                               product.Name,
                               product.productUniquePrice,
                               product.serviceId,
                           }).ToList();
            return Ok(products);
        }
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("AddProduct")]
        public ActionResult<Product> AddProduct([FromBody] DtoProductCreate dtoProduct)
        {
            if (dtoProduct != null)
            {
                Product product = new Product();
                product.Name = dtoProduct.Name;
                product.productUniquePrice = dtoProduct.productUniquePrice;
                product.serviceId = dtoProduct.serviceId;
                _context.Product.Add(product);
                _context.SaveChanges();
                return Ok("Product added");
            }
            return BadRequest("Product not added");
        }
    }
}
