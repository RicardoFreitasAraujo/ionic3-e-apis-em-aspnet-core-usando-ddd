using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Infra.Persistence.EF;

namespace YouLearn.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly YouLearnContext _context;

        public UnitOfWork(YouLearnContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }
    }
}
