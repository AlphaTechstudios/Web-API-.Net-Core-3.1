using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Common
{
    public interface IUnitOfWork
    {
        void Commit();

    }
}
