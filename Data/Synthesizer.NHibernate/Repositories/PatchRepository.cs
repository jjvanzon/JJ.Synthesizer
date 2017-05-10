using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class PatchRepository : DefaultRepositories.PatchRepository
    {
        private new readonly NHibernateContext _context;

        public PatchRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override Patch TryGetByName(string name)
        {
            Patch entity = _context.Session
                                   .QueryOver<Patch>()
                                   .Where(x => x.Name == name)
                                   .SingleOrDefault();
            return entity;
        }
    }
}
