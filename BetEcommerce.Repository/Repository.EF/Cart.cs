using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    [JsonObject(IsReference =true)]
    public class Cart : BaseORM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<CartItem> CartItems { get; set; }
    }
}
