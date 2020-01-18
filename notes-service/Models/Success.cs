using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Models
{
    public class Success<T> 
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
    }
}
