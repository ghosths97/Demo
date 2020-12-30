using System;
using Xunit;

namespace Demo.Tests
{
    public class UnitTest1
    {

        int add(int n, int m ) => n+m;

        [Fact]
        public void TestAdding_two_numbers()
        {
            // Arrange
            int num1 = 100;
            int num2 = 200;
            int expectedResult = 300;

            // Act
            int testResult = add(num1 , num2);


            // Assert
            Assert.Equal(expectedResult, testResult);
        }

       [Theory]
       [InlineData(12,12,24)]
        [InlineData(0, 0, 0)]
        [InlineData(-1, -3, -4)]
        [InlineData(100, 12, 112)]
        public void TestAddingTheory(int n1, int n2, int result)
        {

            // Act
            int testResult = add(n1, n2);


            // Assert
            Assert.Equal(result, testResult);

        }


    }
}
