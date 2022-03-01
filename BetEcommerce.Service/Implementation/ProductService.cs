using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class ProductService : IProductService
    {
        private BetEcommerceDBContext _context;
        public ProductService(BetEcommerceDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }
    }
}
