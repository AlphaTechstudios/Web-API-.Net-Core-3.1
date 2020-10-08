using RefreshToken.Context;
using RefreshToken.Models.Entities;
using Repositories.Common;
using Repositories.Interfaces;

namespace Repositories
{
    public class RefreshTokensRepository : GenericRepository<RefreshTokenModel>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(RefreshTokenContext refreshTokenContext)
            : base(refreshTokenContext)
        {
        }
    }
}
