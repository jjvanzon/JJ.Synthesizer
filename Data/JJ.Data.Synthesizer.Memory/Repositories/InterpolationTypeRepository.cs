using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class InterpolationTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.InterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Block");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Line");
        }
    }
}