using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class EntityPositionRepository : JJ.Data.Synthesizer.DefaultRepositories.EntityPositionRepository
    {
        private new NHibernateContext _context;

        public EntityPositionRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override EntityPosition TryGetByEntityTypeNameAndEntityID(string entityTypeName, int entityID)
        {
            return _context.Session.QueryOver<EntityPosition>()
                                   .Where(x => x.EntityTypeName == entityTypeName)
                                   .Where(x => x.EntityID == entityID)
                                   .SingleOrDefault();
        }
    }
}
