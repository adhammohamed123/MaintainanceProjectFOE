using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ErrorModel
{
   public record Error(int Code,string message); 
   public record ResponseShape<T>
    {
        public ResponseShape(int StatusCode, string message, Dictionary<string,string> errors, List<T> data)
        {
            this.StatusCode = StatusCode;
            this.message = message;
            this.errors = errors;
            this.data = data;
        }

        public int StatusCode { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> errors { get; set; }
        public List<T> data { get; set; }
    }
}
