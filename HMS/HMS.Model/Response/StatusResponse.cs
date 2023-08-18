using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Response
{
    public class StatusResponse<T> where T : class
    {
        public T? Value { get; set; }
        public IEnumerable<T>? ListValue { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
