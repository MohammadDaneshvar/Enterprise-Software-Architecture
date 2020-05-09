using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data
{
    public interface IDbContext
    {
        void Begin();
        void Commit();
        int SaveChanges();
        void Rollback();
    }
}
