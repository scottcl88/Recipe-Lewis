using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class ServiceResult
    {
        public ServiceResult()
        {

        }
        public ServiceResult(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
        public static ServiceResult SuccessResult = new ServiceResult(true);
        public static ServiceResult FailureResult = new ServiceResult(false);
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
