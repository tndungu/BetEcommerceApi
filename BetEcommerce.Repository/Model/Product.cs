using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class Product : BaseORM
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ImageId { get; set; }
        public decimal price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
