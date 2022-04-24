using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Service.Communication
{
    //TODO: A Response when manipulate data (success => entity, fail=> fault message)
    public abstract class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Resource { get; private set; }
        protected BaseResponse(T resource)
        {
            Success = true;
            Message = string.Empty;
            Resource = resource;
        }
        protected BaseResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }
}