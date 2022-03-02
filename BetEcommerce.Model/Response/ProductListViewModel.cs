using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Model.Response
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        public int? NextPointer { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
