using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly BetEcommerceDBContext _context;
        public ProductService(BetEcommerceDBContext context)
        {
            _context = context;
        }
        public async Task<ProductListViewModel> GetProducts(PointerParams @params)
        {
            var products = await _context.Products
                .OrderBy(x => x.Id)
                .Where(x => x.Id > @params.Pointer)
                .Take(@params.Count)
                .ToListAsync();

            ProductListViewModel result = new()
            {
                NextPointer = products.Any() ? products.LastOrDefault()?.Id : -1,
                Products = JsonConvert.DeserializeObject<List<ProductViewModel>>(JsonConvert.SerializeObject(products))
            };

            return result;
        }
    }
}
