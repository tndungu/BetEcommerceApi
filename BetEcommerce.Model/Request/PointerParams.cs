using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Model.Request
{
    public class PointerParams
    {
        public int Count { get; set; } = 50;
        public int Pointer { get; set; } = 0;
    }
}
