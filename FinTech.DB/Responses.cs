using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB
{
  public  class Responses<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Responses(int statusCode, bool succeeded, string message, T data)
        {
            Data = data;
            Succeeded = succeeded;
            Message = message;
            StatusCode = statusCode;
        }
        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Responses<T> Fail(string errorMessage, int statusCode = 404)
        {
            return new Responses<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static Responses<T> Success(string successMessage, T data, int statusCode = 200)
        {
            return new Responses<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        /// <summary>
        /// public override string ToString() => JsonConvert.SerializeObject(this);
        /// </summary>
        public Responses() { }

    }
}
