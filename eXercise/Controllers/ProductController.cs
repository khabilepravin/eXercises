using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eXercise.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger,
                                IProductService productRepository)
        {
            _logger = logger;
            _productService = productRepository;
        }


        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Product>>> GetSortedProducts([FromQuery]string sortOption)
        {
            if (IsValidSortOption(sortOption) == false)
            {
                return BadRequest("Invalid sort option");
            }
           
           var products = await _productService.GetSortedProductsAsync(sortOption);

            return Ok(products);
        }


        private bool IsValidSortOption(string sortOption)
        {
            var validOptions = new string[] { "Low", "High", "Ascending", "Descending", "Recommended" };

            var selectedOption = validOptions.FirstOrDefault(option => string.Compare(option, sortOption, ignoreCase: true) == 0);

            return selectedOption != null;
        }

    }
}
