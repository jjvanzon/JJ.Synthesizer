using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface IPatchRepository : IRepository<Patch, int>
    {
        Patch TryGetByName(string name);
        Patch GetByName(string name);
    }
}
