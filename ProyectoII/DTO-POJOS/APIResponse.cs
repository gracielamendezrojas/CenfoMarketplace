using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_POJOS
{
    public class APIResponse
    {
        public object Data { get; set; }
        public object Status { get; set; }
        public string Message { get; set; }

        public object TransactionDate { get; set; }
    }
}
