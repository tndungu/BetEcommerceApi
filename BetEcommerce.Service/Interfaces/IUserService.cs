using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse> Authenticate(UserRequest userRequest);
        Task<ApiResponse> Register(UserRequest user);
    }
}
