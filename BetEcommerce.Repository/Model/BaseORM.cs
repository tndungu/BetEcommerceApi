using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class BaseORM
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
