using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Interfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(string message, string emailAddress);
    }
}
