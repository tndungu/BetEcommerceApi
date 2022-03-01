using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class Cart : BaseORM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
