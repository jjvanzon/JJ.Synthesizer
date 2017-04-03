using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DimensionRepository : RepositoryBase<Dimension, int>, IDimensionRepository
    {
        public DimensionRepository(IContext context)
            : base(context)
        { }
    }
}
