using System.Collections.Generic;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class OperatorRepository : DefaultRepositories.OperatorRepository
    {
        private new NHibernateContext _context;

        public OperatorRepository(IContext context) 
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Operator> GetManyByOperatorTypeID(int operatorTypeID)
        {
            return _context.Session.QueryOver<Operator>()
                                   .Where(x => x.OperatorType.ID == operatorTypeID)
                                   .List();
        }
    }
}
