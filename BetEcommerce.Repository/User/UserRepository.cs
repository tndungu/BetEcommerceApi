using BetEcommerce.Repository.Repository.EF;

namespace BetEcommerce.Repository.User
{
    public class UserRepository : GenericRepository<Repository.EF.User, int>,IUserRepository
    {
        public UserRepository(BetEcommerceDBContext context) : base(context)
        {
        }

        public async Task<Repository.EF.User> FindUserByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }
    }
}
