using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InterpolationTypeRepository : RepositoryBase<InterpolationType, int>, IInterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        { }
    }
}
