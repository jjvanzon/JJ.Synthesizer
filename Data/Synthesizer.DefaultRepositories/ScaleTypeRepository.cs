using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ScaleTypeRepository : RepositoryBase<ScaleType, int>, IScaleTypeRepository
    {
        public ScaleTypeRepository(IContext context)
            : base(context)
        { }
    }
}
