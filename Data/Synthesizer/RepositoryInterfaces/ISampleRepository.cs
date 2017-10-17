using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface ISampleRepository : IRepository<Sample, int>
    {
        byte[] TryGetBytes(int sampleID);
        byte[] GetBytes(int sampleID);
        void SetBytes(int sampleID, byte[] bytes);
        IList<Sample> GetAll();
    }
}
