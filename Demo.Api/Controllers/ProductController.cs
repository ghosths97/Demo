using Demo.Exceptions;
using Demo.Filters;
using Demo.Models;
using Demo.Services;
using Demo.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
  //  [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService { get; set; }
        public ILogger<ProductController> _logger { get; }

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = Permissions.EditProduct)]
        public IActionResult Get(int productID)
        {
            var product = _productService.GetProduct(productID);
            if (product == null)
            {
                _logger.LogError("Product not found for {productID}", productID);
                throw new DemoException("Product not found", System.Net.HttpStatusCode.NotFound);
            }

            _logger.LogInformation("Product returned {productID}", productID);
            return Ok(product);
        }


        [HttpGet]
        [Route("All")]
        [Authorize(Policy = Permissions.EditProduct)]
        public IActionResult All()
        {
            return Ok( _productService.GetAll());        
        }

        [HttpPut]
        [Authorize(Policy = Permissions.EditProduct)]
        public IActionResult Put(Product product)
        {
            var p = _productService.GetProduct(product.id);
            if (p == null)
            {
                _logger.LogError("Product does not exist for {productID}", product.id);
                throw new DemoException("Product does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            _productService.Update(product);
            _logger.LogInformation("Product Updated {productID}", product.id);
            return Ok(product);
        }


        [HttpPost]
        [Authorize(Policy = Permissions.AddProduct)]
        public IActionResult Post(Product product)
        {
            product.id = 0;
            var p = _productService.GetProduct(product.id);
            if (p != null)
            {
                _logger.LogError("Product already exist for {productID}", product.id);
                throw new DemoException("Product already exist", System.Net.HttpStatusCode.BadRequest);
            }

            _productService.AddProduct(product);
            _logger.LogInformation("Product Created {productID}", product.id);
            return Ok(product);

            
        }

        [HttpDelete]
        [Authorize(Policy = Permissions.EditProduct)]
        public IActionResult Delete(int productID)
        {
            var p = _productService.GetProduct(productID);
            if (p == null)
            {
                _logger.LogError("Product does not exist for {productID}", productID);
                throw new DemoException("Product does not exist", System.Net.HttpStatusCode.BadRequest);
            }
            //var p = product;
            _productService.Delete(productID);
            _logger.LogInformation("Product Deleted {productID}", productID);
            return Ok(productID);


        }

    }
}
