using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    [UsedImplicitly]
    public class OperatorRepository : DefaultRepositories.OperatorRepository
    {
        private new readonly NHibernateContext _context;

        public OperatorRepository(IContext context) : base(context) => _context = (NHibernateContext)context;

        public override IList<Operator> GetAll() => _context.Session.QueryOver<Operator>().List();

        public override IList<Operator> GetManyByUnderlyingPatchID(int underlyingPatchID) => _context.Session.QueryOver<Operator>()
                                                                                                     .Where(x => x.UnderlyingPatch.ID == underlyingPatchID)
                                                                                                     .List();
    }
}