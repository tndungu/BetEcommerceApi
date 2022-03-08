using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Model.Request
{
    public class PointerParams
    {
        private int _maxItemsPerPage = 10;
        private int itemsPerPage;
        public int Count 
        { 
            get => itemsPerPage; 
            set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage: value; 
        }
        public int Pointer { get; set; } = 1;
    }
}
