using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ScaleTypeRepository : RepositoryBase<ScaleType, int>, IScaleTypeRepository
    {
        public ScaleTypeRepository(IContext context)
            : base(context)
        { }
    }
}
