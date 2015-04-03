using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class EntityPositionRepository : JJ.Persistence.Synthesizer.DefaultRepositories.EntityPositionRepository
    {
        private new MemoryContext _context;

        public EntityPositionRepository(IContext context)
            : base(context)
        {
            _context = (MemoryContext)context;
        }

        public override EntityPosition TryGetByEntityTypeNameAndID(string entityTypeName, int entityID)
        {
            return _context.GetAll<EntityPosition>()
                           .Where(x => x.EntityTypeName == entityTypeName &&
                                       x.EntityID == entityID)
                           .SingleOrDefault();
        }
    }
}
