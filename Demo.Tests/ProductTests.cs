using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Test_Product_Expiry()
        {
            // Arrange
            var productionDate = DateTime.Now;
            Product p = new Product()
            {
                Id = 1,
                name  = "p1",
                production = productionDate,
                ExpiresInDays = 365
            };
            int expMonth = 12;
            int expYear = 2021;

            // Act
            int tMonth = p.ExpiryDate().Month;
            int tYear = p.ExpiryDate().Year;


            // Assert
            Assert.Equal(expMonth, tMonth);
            Assert.Equal(expYear, tYear);
        }

    }
}
