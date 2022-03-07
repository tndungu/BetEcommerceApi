using BetEcommerce.Api.Controllers.API.V1;
using BetEcommerce.Model.API;
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
    public class UserTests
    {
        Mock<IUserService> UserServiceMock = new Mock<IUserService>();
        private UserController UserController;

        [SetUp]
        public void Setup()
        {
            UserServiceMock.Setup(x => x.Authenticate(new UserRequest { Email = "tmndungu@gmail.com", Password = "password" }))
                .ReturnsAsync(new UserResponse
                {
                    Id = 3,
                    Email = "tmndungu@gmail.com",
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJuYmYiOjE2NDY2MzkzNjAsImV4cCI6MTY0NjcyNTc2MCwiaWF0IjoxNjQ2NjM5MzYwfQ.BVTrGA2yA7VkCCX4DAeIFM5jxRpDVMzkuDCenV9sUig"
                });
        }
        [Test]
        public async Task should_Login()
        {
            UserController = new UserController(UserServiceMock.Object);

            var result = await UserController.Authenticate(new UserRequest { Email = "tmndungu@gmail.com", Password = "password" });

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
