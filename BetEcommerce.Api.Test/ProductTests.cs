using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BetEcommerce.Api.Test
{
    public class ProductTests
    {
        Mock<IProductService> ProductServiceMock = new();
        private ProductController ProductController;

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task should_Get_List_Of_Ten_Products()
        {
            //Arrange
            var req = new PointerParams { Pointer = 1, Count = 10 };
            ProductServiceMock.Setup(x => x.GetProducts(req))
                .ReturnsAsync(It.IsAny<ProductListViewModel>());

            ProductController = new ProductController(ProductServiceMock.Object);

            //Act
            var result = await ProductController.GetProducts(req) as OkObjectResult;
            var response = result.Value as ApiResponse<ProductListViewModel>;
            var data = response.data;

            //Assert
            Assert.AreEqual(10, data.Products.Count);
        }
    }
}