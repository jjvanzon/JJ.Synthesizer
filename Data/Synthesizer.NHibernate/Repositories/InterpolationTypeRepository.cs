using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    [UsedImplicitly]
    public class InterpolationTypeRepository : DefaultRepositories.InterpolationTypeRepository
    {
        private new readonly NHibernateContext _context;

        public InterpolationTypeRepository(IContext context) : base(context) => _context = (NHibernateContext)context;

        public override IList<InterpolationType> GetAll() => _context.Session.QueryOver<InterpolationType>().List();
    }
}