using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DimensionRepository : RepositoryBase<Dimension, int>, IDimensionRepository
    {
        public DimensionRepository(IContext context)
            : base(context)
        { }
    }
}
