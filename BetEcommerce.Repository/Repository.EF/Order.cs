using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class Order : BaseORM
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> OrderItems { get; set;}
    }
}
