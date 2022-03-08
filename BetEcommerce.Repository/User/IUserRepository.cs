namespace BetEcommerce.Repository.User
{
    public interface IUserRepository : IGenericRepository<Repository.EF.User,int> 
    {
        Task<Repository.EF.User> FindUserByEmail(string email);
    }
}
