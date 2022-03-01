using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Service.Interfaces;
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
    public class UserTests
    {
        Mock<IUserService> UserServiceMock = new Mock<IUserService>();
        private UserController UserController;

        [SetUp]
        public void Setup()
        {
            UserServiceMock.Setup(x => x.Authenticate(new UserRequest { Email = "antony@gmail.com", Password = "Password1" }))
                .ReturnsAsync(new ApiResponse(HttpStatusCode.OK)
                {
                    ResponseObject = new
                    {
                        User = new
                        {
                            Id = 1,
                            Name = "Antony",
                            Email = "antony@gmail.com",
                            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiMSIsIm5iZiI6MTYxOTE4MTU3NCwiZXhwIjoxNjE5MjY3OTc0LCJpYXQiOjE2MTkxODE1NzR9.rNlh1m_vlYKL2_PdqtaZR2GFwKVYYV3_1EEe1cGZBww"
                        }
                    }
                });

        }
        [Test]
        public async Task should_Login()
        {
            UserController = new UserController(UserServiceMock.Object);

            var result = await UserController.Authenticate(new UserRequest { Email = "antony@gmail.com", Password = "Password1" });

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseCode);
        }
    }
}
