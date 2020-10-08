using RefreshToken.Models.Entities;

namespace Managers.Interfaces
{
    public interface IRefreshTokensManager
    {
        RefreshTokenModel GetToken(string refreshToken);

        RefreshTokenModel UpdateToken(string refreshToken);

        RefreshTokenModel AddToken(string userEmail);
    }
}
