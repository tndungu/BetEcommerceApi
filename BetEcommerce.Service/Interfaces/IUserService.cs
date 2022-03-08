using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
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
        Task<UserResponse> Authenticate(UserRequest userRequest);
        Task<bool> Register(UserRequest userRequest);
        Task<User> GetById(int id);
    }
}
