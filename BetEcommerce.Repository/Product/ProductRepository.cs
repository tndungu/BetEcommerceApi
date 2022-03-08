using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Repository.EF;
using Microsoft.EntityFrameworkCore;

namespace BetEcommerce.Repository.Product
{
    public class ProductRepository : GenericRepository<Repository.EF.Product, int>, IProductRepository
    {
        public ProductRepository(BetEcommerceDBContext context) : base(context)
        {
        }
        public async Task<List<Repository.EF.Product>> GetPagedProductsList(PointerParams @params)
        {
            var productList = await context.Products
            .OrderBy(x => x.Id)
            .Where(x => x.Id > @params.Pointer)
            .Take(@params.Count).ToListAsync();

            return productList;
        }
    }
}
