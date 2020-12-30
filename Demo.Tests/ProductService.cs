using Demo.Controllers;
using Demo.Models;
using Demo.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class ProductService
    {

        private readonly ProductController productController;
        private readonly Mock<IProductService> productService;
        private readonly Mock<ILogger<ProductController>> loggerService;

        public ProductService()
        {

            productService = new Mock<IProductService>();
            loggerService = new Mock<ILogger<ProductController>>();

            productController = new ProductController(productService.Object, loggerService.Object);
        }

        [Fact]
        public  void Test_Get_Product_When_Available()
        {
            // a
            int productid = 1;
            Product p = new Product()
            {
                id = productid,
                name = "pr1",
                production =  DateTime.Now,
                ExpiresInDays = 7
            };
            productService.Setup(s => s.GetProduct(productid)).Returns(p);

            //// act
            //var product  = productController.Get(1);


            //// Assert
            //Assert.Equal(productid, product.id);
            //Assert.Equal(1, product.ExpiryDate().Month);
        }

    }
}
