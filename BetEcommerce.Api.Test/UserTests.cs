using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

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
        public async Task should_Login_WhenCredentialsAreValid()
        {
            //Arrange 
            var userResponse = new UserResponse
            {
                Id = 1,
                Email = "username@gmail.com",
                Token = "TEST_ejcyNTc2MCwiaWF0IjoxNjQ2NjM5MzYwfQ.BVTrGA2yA7VkCCX4DAeIFM5jxRpDVMzkuDCenV9sUig"
            };

            var usr = new UserRequest { Email = "tmndungu@gmail.com", Password = "password" };

            UserServiceMock.Setup(x => x.Authenticate(usr))
                .ReturnsAsync(userResponse);

            var UserController = new UserController(UserServiceMock.Object);

            //Act
            var result = await UserController.Authenticate(usr) as OkObjectResult;
            var res = result.Value as ApiResponse<UserResponse>;
            var userData = res.data;

            //Assert
            Assert.IsNotNull(userData);
            Assert.AreEqual(JsonConvert.SerializeObject(userResponse), JsonConvert.SerializeObject(userData));
        }
        [Test]
        public async Task should_ThrowException_WhenLoginIsInvalid()
        {
            //Arrange 
            var usr = new UserRequest { Email = "tmndungu@gmail.com", Password = "incorrect" };

            UserServiceMock.Setup(x => x.Authenticate(usr))
                .Throws(new HttpException(System.Net.HttpStatusCode.Unauthorized));

            var UserController = new UserController(UserServiceMock.Object);

            //Act
            var result = await UserController.Authenticate(usr) as ObjectResult;
            var res = result.Value as ApiResponse<bool>;

            //Assert
            Assert.AreNotEqual(200,res.statusCode);
        }

        [Test]
        public async Task should_Register()
        {
            //Arrange 
            var usr = new UserRequest { Email = "tmndungu@gmail.com", Password = "password" };

            UserServiceMock.Setup(x => x.Register(usr))
                .ReturnsAsync(true);

            var UserController = new UserController(UserServiceMock.Object);

            //Act
            var result = await UserController.Register(usr) as OkObjectResult;
            var res = result.Value as ApiResponse<bool>;

            //Assert
            Assert.AreEqual(true,res.data);
        }
    }
}
