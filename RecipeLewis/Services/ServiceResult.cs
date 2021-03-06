﻿using System.Collections.Generic;

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

    public class ServiceResult<T>
    {
        public ServiceResult(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }

        public static ServiceResult SuccessResult = new ServiceResult(true);
        public static ServiceResult FailureResult = new ServiceResult(false);
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> DataList { get; set; }
        public T Data { get; set; }
    }
}