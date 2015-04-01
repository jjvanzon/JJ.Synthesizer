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

        public EntityPosition GetByEntityTypeNameAndID(string entityTypeName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
