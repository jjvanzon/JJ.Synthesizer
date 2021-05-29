using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    [UsedImplicitly]
    public class DimensionRepository : RepositoryBase<Dimension, int>, IDimensionRepository
    {
        public DimensionRepository(IContext context)
            : base(context)
        { }
    }
}
