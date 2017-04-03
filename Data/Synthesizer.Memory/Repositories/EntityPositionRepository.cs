using JJ.Framework.Data;
using JJ.Framework.Data.Memory;
using System.Linq;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class EntityPositionRepository : DefaultRepositories.EntityPositionRepository
    {
        private new readonly MemoryContext _context;

        public EntityPositionRepository(IContext context)
            : base(context)
        {
            _context = (MemoryContext)context;
        }

        public override EntityPosition TryGetByEntityTypeNameAndEntityID(string entityTypeName, int entityID)
        {
            return _context.GetAll<EntityPosition>()
                           .Where(x => x.EntityTypeName == entityTypeName &&
                                       x.EntityID == entityID)
                           .SingleOrDefault();
        }
    }
}
