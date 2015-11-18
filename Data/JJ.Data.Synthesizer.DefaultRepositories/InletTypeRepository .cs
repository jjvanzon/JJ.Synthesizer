using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InletTypeRepository : RepositoryBase<InletType, int>, IInletTypeRepository
    {
        public InletTypeRepository(IContext context)
            : base(context)
        { }
    }
}
