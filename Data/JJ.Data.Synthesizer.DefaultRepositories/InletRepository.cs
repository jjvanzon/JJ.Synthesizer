using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InletRepository : RepositoryBase<Inlet, int>, IInletRepository
    {
        public InletRepository(IContext context)
            : base(context)
        { }
    }
}
