using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SampleDataTypeRepository : RepositoryBase<SampleDataType, int>, ISampleDataTypeRepository
    {
        public SampleDataTypeRepository(IContext context)
            : base(context)
        { }
    }
}
