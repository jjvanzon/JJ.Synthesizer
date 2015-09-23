using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ScaleRepository : RepositoryBase<Scale, int>, IScaleRepository
    {
        public ScaleRepository(IContext context)
            : base(context)
        { }
    }
}
