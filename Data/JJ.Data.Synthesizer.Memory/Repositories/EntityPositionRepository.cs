using JJ.Framework.Data;
using JJ.Framework.Data.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class EntityPositionRepository : JJ.Data.Synthesizer.DefaultRepositories.EntityPositionRepository
    {
        private new MemoryContext _context;

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
