using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class JsonMessage
    {
        public string message;
        public int number_code;
        public bool status;
        public object? result;
        public T? GetResultObject<T>()
        {
            if (result == null) return default(T);
            return ((JObject)result).ToObject<T>();
        }
        public List<T>? GetResultArray<T>()
        {
            if (result == null) return null;
            return ((JArray)result).ToObject<List<T>>();
        }
    }
}
