using RefreshToken.Models.Enums;
using System;

namespace RefreshToken.Models.Entities
{
    public class UserModel: BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public StatusEnum Status { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpires { get; set; }

        public string RefreshToken { get; set; }
    }
}
