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
        EntityPosition TryGetByEntityTypeNameAndID(string entityTypeName, int id);
        EntityPosition GetByEntityTypeNameAndID(string entityTypeName, int id);
        void DeleteByEntityTypeAndEntityID(string entityTypeName, int id);
    }
}
