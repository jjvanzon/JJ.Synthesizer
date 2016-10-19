using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SampleDataTypeRepository : RepositoryBase<SampleDataType, int>, ISampleDataTypeRepository
    {
        public SampleDataTypeRepository(IContext context)
            : base(context)
        { }
    }
}
