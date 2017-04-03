using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface ISampleRepository : IRepository<Sample, int>
    {
        void SetBytes(int id, byte[] bytes);
        byte[] TryGetBytes(int id);
    }
}
