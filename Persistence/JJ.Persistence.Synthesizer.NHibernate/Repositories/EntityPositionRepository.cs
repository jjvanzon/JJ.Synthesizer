using JJ.Framework.Persistence;
using JJ.Framework.Persistence.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Repositories
{
    public class EntityPositionRepository : JJ.Persistence.Synthesizer.DefaultRepositories.EntityPositionRepository
    {
        private new NHibernateContext _context;

        public EntityPositionRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override EntityPosition TryGetByEntityTypeNameAndID(string entityTypeName, int entityID)
        {
            return _context.Session.QueryOver<EntityPosition>()
                                   .Where(x => x.EntityTypeName == entityTypeName)
                                   .Where(x => x.EntityID == entityID)
                                   .SingleOrDefault();
        }
    }
}
