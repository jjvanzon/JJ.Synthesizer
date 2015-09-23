using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class ScaleTypeRepository : DefaultRepositories.ScaleTypeRepository
    {
        public ScaleTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "LiteralFrequency");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Factor");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Exponent");
            RepositoryHelper.EnsureEnumEntity(this, 4, "SemiTone");
        }
    }
}