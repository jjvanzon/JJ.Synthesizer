using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OutletRepository : RepositoryBase<Outlet, int>, IOutletRepository
    {
        public OutletRepository(IContext context)
            : base(context)
        { }
    }
}
