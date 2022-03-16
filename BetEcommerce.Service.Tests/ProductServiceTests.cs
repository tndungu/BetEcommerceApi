using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Product;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Implementation;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BetEcommerce.Service.Tests
{
    public class ProductServiceTests
    {
        private ProductService _productService;
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task Should_Return_A_List_Of_5_Products()
        {
            //Arrange
            var productList = new List<Product>()
            {
                new Product() { Id = 1, Name ="PROD 1",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 2, Name ="PROD 2",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 3, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 4, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 5, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 }
            };
            _productService = new ProductService(_productRepository.Object);
            var pointerParams = new PointerParams { Pointer = 1, Count = 10 };
            _productRepository.Setup(x => x.GetPagedProductsList(pointerParams))
                .ReturnsAsync(productList);

            //Act
            var products = await _productService.GetProducts(pointerParams);

            //Assert
            Assert.AreEqual(5,products.Products.Count());
        }
        [Test]
        public async Task Should_Return_NextPointer_Value_Of_5()
        {
            //Arrange
            var productList = new List<Product>()
            {
                new Product() { Id = 1, Name ="PROD 1",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 2, Name ="PROD 2",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 3, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 4, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                new Product() { Id = 5, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 }
            };
            _productService = new ProductService(_productRepository.Object);
            var pointerParams = new PointerParams { Pointer = 1, Count = 10 };
            _productRepository.Setup(x => x.GetPagedProductsList(pointerParams))
                .ReturnsAsync(productList);

            //Act
            var products = await _productService.GetProducts(pointerParams);

            //Assert
            products.NextPointer.Should().Be(5);
        }
    }
}
