using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Infra.Transactions
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}
