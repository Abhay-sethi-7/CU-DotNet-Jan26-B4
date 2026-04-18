using Xunit;
using NorthwindCatalog.Services.DTOs;

namespace NorthwindCatalog.Tests
{
    public class ProductTests
    {
        [Fact]
        public void InventoryValue_Should_Return_Correct_Value()
        {
            // Arrange
            var product = new ProductDto
            {
                UnitPrice = 100,
                UnitsInStock = 5
            };

            // Act
            var result = product.InventoryValue;

            // Assert
            Assert.Equal(500, result);
        }
        [Fact]
        public void InventoryValue_Should_Be_Zero_When_Stock_Is_Zero()
        {
            var product = new ProductDto
            {
                UnitPrice = 100,
                UnitsInStock = 0
            };

            var result = product.InventoryValue;

            Assert.Equal(0, result);
        }
        [Fact]
        public void InventoryValue_Should_Be_Zero_When_Price_Is_Zero()
        {
            var product = new ProductDto
            {
                UnitPrice = 0,
                UnitsInStock = 10
            };

            var result = product.InventoryValue;

            Assert.Equal(0, result);
        }
        [Fact]
        public void InventoryValue_Should_Handle_Large_Values()
        {
            var product = new ProductDto
            {
                UnitPrice = 1000,
                UnitsInStock = 100
            };

            var result = product.InventoryValue;

            Assert.Equal(100000, result);
        }
    }
}