using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_POJOS
{
    public class Login : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
