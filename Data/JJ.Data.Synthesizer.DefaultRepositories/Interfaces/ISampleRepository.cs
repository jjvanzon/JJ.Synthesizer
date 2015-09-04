using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface ISampleRepository : IRepository<Sample, int>
    {
        void SetBytes(int id, byte[] bytes);
        byte[] TryGetBytes(int id);
    }
}
