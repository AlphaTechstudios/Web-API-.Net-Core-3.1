using RefreshToken.Context;
using System;

namespace Repositories.Common
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private RefreshTokenContext dbContext;
        private bool disposed = false;

        public UnitOfWork(RefreshTokenContext refreshTokenContext)
        {
            dbContext = refreshTokenContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool isDisposed)
        {
            if (!disposed)
            {
                if (!isDisposed)
                {
                    dbContext.Dispose();
                }
            }

            disposed = true;
        }
    }
}
