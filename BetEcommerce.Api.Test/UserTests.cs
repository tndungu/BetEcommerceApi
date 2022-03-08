using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
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
            var userResponse = new UserResponse
            {
                Id = 1,
                Email = "username@gmail.com",
                Token = "TEST_ejcyNTc2MCwiaWF0IjoxNjQ2NjM5MzYwfQ.BVTrGA2yA7VkCCX4DAeIFM5jxRpDVMzkuDCenV9sUig"
            };

            UserServiceMock.Setup(x => x.Authenticate(It.IsAny<UserRequest>()))
                .Returns(Task.Run(() => userResponse));

            var UserController = new UserController(UserServiceMock.Object);

            //Act
            var result = await UserController.Authenticate(It.IsAny<UserRequest>()) as OkObjectResult;
            var res = result.Value as ApiResponse<UserResponse>;
            var userData = res.data;

            //Assert
            Assert.IsNotNull(userData);
            Assert.AreEqual(JsonConvert.SerializeObject(userResponse), JsonConvert.SerializeObject(userData));
        }
        


    }
}
