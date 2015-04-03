using JJ.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IEntityPositionRepository : IRepository<EntityPosition, int>
    {
        EntityPosition TryGetByEntityTypeNameAndID(string entityTypeName, int id);
        EntityPosition GetByEntityTypeNameAndID(string entityTypeName, int id);
    }
}
