using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class EntityPointRepository : RepositoryBase<EntityPoint, int>, IEntityPointRepository 
    {
        public EntityPointRepository(IContext context)
            : base(context)
        { }

        public EntityPoint GetByEntityTypeNameAndID(string entityTypeName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
