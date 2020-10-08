using RefreshToken.Models.Entities;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IUsersRepository: IGenericRepository<UserModel>
    {
    }
}
