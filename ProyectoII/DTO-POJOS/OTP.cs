using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_POJOS
{
    public class OTP: BaseEntity
    {
        public int Id { get; set; }
        public string OTPNumber { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

    }
}
