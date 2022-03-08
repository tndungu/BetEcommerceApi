using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Product;
using BetEcommerce.Service.Interfaces;
using Newtonsoft.Json;

namespace BetEcommerce.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;
        public async Task<ProductListViewModel> GetProducts(PointerParams @params)
        {
            var products = await _productRepository.GetPagedProductsList(@params);

            ProductListViewModel result = new()
            {
                NextPointer = products.Any() ? products.LastOrDefault()?.Id : -1,
                Products = JsonConvert.DeserializeObject<List<ProductViewModel>>(JsonConvert.SerializeObject(products))
            };
            return result;
        }
    }
}
