using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class User : BaseORM
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(250)]
        public byte[] PasswordHash { get; set; }
        [MaxLength(250)]
        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
