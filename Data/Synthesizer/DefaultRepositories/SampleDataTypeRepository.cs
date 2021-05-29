using JetBrains.Annotations;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    [UsedImplicitly]
    public class SampleDataTypeRepository : RepositoryBase<SampleDataType, int>, ISampleDataTypeRepository
    {
        public SampleDataTypeRepository(IContext context)
            : base(context)
        { }
    }
}
