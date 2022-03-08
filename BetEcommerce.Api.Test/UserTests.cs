using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;

namespace BetEcommerce.Api.Test
{
    public class UserTests
    {
         Mock<IUserService> UserServiceMock = new Mock<IUserService>();

        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public async Task should_Login()
        {
            //Arrange 
            var user = new UserResponse
            {
                Id = 1,
                Email = "username@gmail.com",
                Token = "TEST_ejcyNTc2MCwiaWF0IjoxNjQ2NjM5MzYwfQ.BVTrGA2yA7VkCCX4DAeIFM5jxRpDVMzkuDCenV9sUig"
            };
            var u = new UserRequest { Email = "username@gmail.com", Password = "password" };
            UserServiceMock.Setup(x => x.Authenticate(u))
                .Returns(Task.Run(() => user));
            var UserController = new UserController(UserServiceMock.Object);

            //Act
            var result = await UserController.Authenticate(u);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, GetHttpStatusCode(result));
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
