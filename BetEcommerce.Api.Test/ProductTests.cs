using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            var products = new ProductListViewModel();
            ProductServiceMock.Setup(x => x.GetProducts(new PointerParams { Pointer = 1, Count = 10 }))
                .ReturnsAsync(It.IsAny<ProductListViewModel>());

            ProductController = new ProductController(ProductServiceMock.Object);
            //Act
            var req = new PointerParams { Pointer = 1, Count = 10 };
            var result = ProductController.GetProducts(req);
            //Assert
            Assert.AreEqual(10, result);
        }

        public static HttpStatusCode GetHttpStatusCode(IActionResult functionResult)
        {
            try
            {
                return (HttpStatusCode)functionResult
                    .GetType()
                    .GetProperty("StatusCode")
                    .GetValue(functionResult, null);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

        }
    }
}