using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class InletTypeRepository : DefaultRepositories.InletTypeRepository
    {
        public InletTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Signal");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Frequency");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Volume");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Frequencies");
            RepositoryHelper.EnsureEnumEntity(this, 5, "Volumes");
            RepositoryHelper.EnsureEnumEntity(this, 6, "VibratoSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 7, "VibratoDepth");
            RepositoryHelper.EnsureEnumEntity(this, 8, "TremoloSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 9, "TremoloDepth");
       }
    }
}