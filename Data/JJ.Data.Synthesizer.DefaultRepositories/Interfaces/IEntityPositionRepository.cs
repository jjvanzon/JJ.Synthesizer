using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IEntityPositionRepository : IRepository<EntityPosition, int>
    {
        EntityPosition TryGetByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
        EntityPosition GetByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
        void DeleteByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
    }
}
