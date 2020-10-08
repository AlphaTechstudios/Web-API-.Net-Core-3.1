using RefreshToken.Context;
using RefreshToken.Models.Entities;
using Repositories.Common;
using Repositories.Interfaces;

namespace Repositories
{
    public class UsersRepository : GenericRepository<UserModel>, IUsersRepository
    {
        public UsersRepository(RefreshTokenContext refreshTokenContext) :
            base(refreshTokenContext)
        {
        }
    }
}
