using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IEntityPositionRepository : IRepository<EntityPosition, int>
    {
        EntityPosition TryGetByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
        EntityPosition GetByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
        void DeleteByEntityTypeNameAndEntityID(string entityTypeName, int entityID);
    }
}
