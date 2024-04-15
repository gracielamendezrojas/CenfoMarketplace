
using System;

namespace DTO_POJOS
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string PreferredMethod { get; set; }
        public string Avatar { get; set; }

        public string Status { get; set; }
        public DateTime DOB { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        public string Role { get; set; }

    }
}
