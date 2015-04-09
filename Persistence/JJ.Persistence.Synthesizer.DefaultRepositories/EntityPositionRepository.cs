using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class EntityPositionRepository : RepositoryBase<EntityPosition, int>, IEntityPositionRepository 
    {
        public EntityPositionRepository(IContext context)
            : base(context)
        { }

        public EntityPosition GetByEntityTypeNameAndID(string entityTypeName, int entityID)
        {
            EntityPosition entity = TryGetByEntityTypeNameAndID(entityTypeName, entityID);
            if (entity == null)
            {
                throw new Exception(String.Format("EntityPosition with EntityTypeName '{0}' and EntityID '{1}' not found.", entityTypeName, entityID));
            }
            return entity;
        }

        public virtual EntityPosition TryGetByEntityTypeNameAndID(string entityTypeName, int entityID)
        {
            return _context.Query<EntityPosition>()
                           .Where(x => x.EntityTypeName == entityTypeName)
                           .Where(x => x.EntityID == entityID)
                           .SingleOrDefault();
        }

        public virtual void DeleteByEntityTypeAndEntityID(string entityTypeName, int id)
        {
            EntityPosition entity = GetByEntityTypeNameAndID(entityTypeName, id);
            Delete(entity);
        }
    }
}
