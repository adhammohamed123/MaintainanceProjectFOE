using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ErrorModel
{
   public record Error(int StatusCode,string message); 
}
