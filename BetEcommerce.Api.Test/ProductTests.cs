using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
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
        public async Task should_Get_List_Of_5_Products()
        {
            //Arrange
            var productList = new List<ProductViewModel>()
                {
                    new ProductViewModel() { Id = 1, Name ="PROD 1",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 2, Name ="PROD 2",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 3, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 4, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 5, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 }
                };
            var productListView = new ProductListViewModel()
            {
                NextPointer = 5,
                Products = productList
            };
                
            var req = new PointerParams { Pointer = 1, Count = 10 };
            ProductServiceMock.Setup(x => x.GetProducts(req))
                .ReturnsAsync(productListView);

            ProductController = new ProductController(ProductServiceMock.Object);

            //Act
            var result = await ProductController.GetProducts(req) as OkObjectResult;
            var response = result.Value as ApiResponse<ProductListViewModel>;
            var data = response.data;

            //Assert
            Assert.AreEqual(5, data.Products.Count);
        }
        [Test]
        public async Task Should_Return_NextPointer_Value_Of_5()
        {
            //Arrange
            var productList = new List<ProductViewModel>()
                {
                    new ProductViewModel() { Id = 1, Name ="PROD 1",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 2, Name ="PROD 2",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 3, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 4, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 },
                    new ProductViewModel() { Id = 5, Name ="PROD 3",Description="PROD 12",ImageId="123456",price=100 }
                };
            var productListView = new ProductListViewModel()
            {
                NextPointer = 5,
                Products = productList
            };

            var req = new PointerParams { Pointer = 1, Count = 10 };
            ProductServiceMock.Setup(x => x.GetProducts(req))
                .ReturnsAsync(productListView);

            ProductController = new ProductController(ProductServiceMock.Object);

            //Act
            var result = await ProductController.GetProducts(req) as OkObjectResult;
            var response = result.Value as ApiResponse<ProductListViewModel>;
            var data = response.data;

            //Assert
            Assert.AreEqual(5, data.NextPointer);
        }
        [Test]
        public async Task should_ThrowException_WhenParametersAreNull()
        {
            //Arrange 
            var req = new PointerParams { Pointer = 1, Count = 10 };

            ProductServiceMock.Setup(x => x.GetProducts(req))
                .Throws(new HttpException(System.Net.HttpStatusCode.BadRequest));

            var productController = new ProductController(ProductServiceMock.Object);

            //Act
            var result = await productController.GetProducts(req) as ObjectResult;
            var res = result.Value as ApiResponse<bool>;

            //Assert
            Assert.AreNotEqual(200, res.statusCode);
        }
    }
}