using Managers.Common;
using Managers.Interfaces;
using RefreshToken.Models.Entities;
using Repositories.Common;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    public class RefreshTokensManager : BaseManager, IRefreshTokensManager
    {
        private readonly IRefreshTokensRepository refreshTokensRepository;

        public RefreshTokensManager(IUnitOfWork unitOfWork, IRefreshTokensRepository refreshTokensRepository)
            : base(unitOfWork)
        {
            this.refreshTokensRepository = refreshTokensRepository;
        }

        public RefreshTokenModel AddToken(string userEmail)
        {
            var now = DateTime.UtcNow;

            var refreshTokenModel = new RefreshTokenModel
            {
                Email = userEmail,
                RefreshToken = Guid.NewGuid().ToString(),
                ModificationDate = now,
                CreationDate = now
            };

            refreshTokensRepository.Insert(refreshTokenModel);
            UnitOfWork.Commit();
            return refreshTokenModel;
        }

        public RefreshTokenModel GetToken(string refreshToken)
        {
            return refreshTokensRepository.Get(x => x.RefreshToken == refreshToken).SingleOrDefault();
        }

        public RefreshTokenModel UpdateToken(string refreshToken)
        {
            var refreshTokenModel = GetToken(refreshToken);
            if(refreshTokenModel != null)
            {
                refreshTokenModel.ModificationDate = DateTime.UtcNow;
                refreshTokenModel.RefreshToken = Guid.NewGuid().ToString();
                refreshTokensRepository.Update(refreshTokenModel);
                UnitOfWork.Commit();
                return refreshTokenModel;
            }
            return null;
        }
    }
}
