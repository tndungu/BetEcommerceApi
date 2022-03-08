using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Repository.User;
using BetEcommerce.Service.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BetEcommerce.Service.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserResponse> Authenticate(UserRequest userRequest)
        {
            var user = await _userRepository.FindUserByEmail(userRequest.Email);
            if (user == null)
                throw new HttpException(HttpStatusCode.Unauthorized, "Username or password incorrect");

            bool valid = VerifyPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt);
            if (!valid)
                throw new HttpException(HttpStatusCode.Unauthorized, "Username or password incorrect");

            var tokenString = new TokenGen().GenerateTokenJWT(user.Id, user.Id, _appSettings.Secret);

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Token = tokenString
            };
        }
        public async Task<bool> Register(UserRequest userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.Password))
                throw new HttpException(HttpStatusCode.BadRequest,"Password is required");

            var userExists = await _userRepository.FindUserByEmail(userRequest.Email);
            if (userExists != null)
                throw new HttpException(HttpStatusCode.BadRequest,$"{userRequest.Email} already exists");

            var user = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(userRequest));
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userRequest.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.InsertAsync(user);
            return true;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }
        #region Private Methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password hash (128 bytes expected).", "passwordHash");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password salt (64 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        #endregion
    }
}
