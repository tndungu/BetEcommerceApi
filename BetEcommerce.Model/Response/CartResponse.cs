using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Model.Response
{
    public class CartResponse
    {
        public int ProductId { get; set; }
        public string ImageId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; } = 0;
    }
}
