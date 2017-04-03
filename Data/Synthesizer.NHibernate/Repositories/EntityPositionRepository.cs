using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class EntityPositionRepository : JJ.Data.Synthesizer.DefaultRepositories.EntityPositionRepository
    {
        private new readonly NHibernateContext _context;

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

        public override int DeleteOrphans()
        {
            var sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            return sqlExecutor.EntityPosition_DeleteOrphans();
        }
    }
}
