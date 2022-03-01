using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Model.API
{
    public class ApiResponse
    {
        public HttpStatusCode HttpResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseObject { get; set; }

        public ApiResponse(HttpStatusCode httpStatusCode, string responseMessage)
        {
            HttpResponseCode = httpStatusCode;
            ResponseMessage = responseMessage;
        }

        public ApiResponse(HttpStatusCode httpStatusCode)
        {
            HttpResponseCode = httpStatusCode;
        }
    }
}
