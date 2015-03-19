using JJ.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IEntityPointRepository : IRepository<EntityPoint, int>
    {
        EntityPoint GetByEntityTypeNameAndID(string entityTypeName, int id);
    }
}
