using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managers.Common
{
    public class BaseManager
    {
        protected IUnitOfWork UnitOfWork { get; }

        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

    }
}
