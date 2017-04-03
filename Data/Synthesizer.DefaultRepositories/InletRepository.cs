using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InletRepository : RepositoryBase<Inlet, int>, IInletRepository
    {
        public InletRepository(IContext context)
            : base(context)
        { }
    }
}
