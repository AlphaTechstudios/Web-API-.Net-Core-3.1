using System;
using System.Collections.Generic;
using System.Text;

namespace RefreshToken.Models.Entities
{
    public class RefreshTokenModel: BaseModel
    {
        public string Email { get; set; }

        public string RefreshToken { get; set; }
    }
}
