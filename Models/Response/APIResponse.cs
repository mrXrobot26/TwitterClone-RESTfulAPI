using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true ;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public void SetResponseInfo(HttpStatusCode statusCode, List<string> errorMessages, object result, bool isSuccess = true)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            ErrorMessages = errorMessages ?? new List<string>();
            Result = result;
        }

    }
}
