using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Linq;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class EntityPositionRepository : RepositoryBase<EntityPosition, int>, IEntityPositionRepository 
    {
        public EntityPositionRepository(IContext context)
            : base(context)
        { }

        public EntityPosition GetByEntityTypeNameAndEntityID(string entityTypeName, int entityID)
        {
            EntityPosition entity = TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);
            if (entity == null)
            {
                throw new Exception(string.Format("EntityPosition with EntityTypeName '{0}' and EntityID '{1}' not found.", entityTypeName, entityID));
            }
            return entity;
        }

        public virtual EntityPosition TryGetByEntityTypeNameAndEntityID(string entityTypeName, int entityID)
        {
            return _context.Query<EntityPosition>()
                           .Where(x => x.EntityTypeName == entityTypeName)
                           .Where(x => x.EntityID == entityID)
                           .SingleOrDefault();
        }

        public virtual void DeleteByEntityTypeNameAndEntityID(string entityTypeName, int entityID)
        {
            EntityPosition entity = GetByEntityTypeNameAndEntityID(entityTypeName, entityID);
            Delete(entity);
        }

        public virtual int DeleteOrphans()
        {
            throw new RepositoryMethodNotImplementedException();
        }
    }
}
