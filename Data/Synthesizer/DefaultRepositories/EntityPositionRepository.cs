using JJ.Framework.Data;
using System;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

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
                throw new Exception($"EntityPosition with EntityTypeName '{entityTypeName}' and EntityID '{entityID}' not found.");
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
