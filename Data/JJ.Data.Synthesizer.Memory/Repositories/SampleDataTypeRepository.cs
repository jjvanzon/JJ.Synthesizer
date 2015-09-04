using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class SampleDataTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.SampleDataTypeRepository
    {
        public SampleDataTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Byte");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Int16");
        }
    }
}