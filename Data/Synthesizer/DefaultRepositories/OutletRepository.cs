using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OutletRepository : RepositoryBase<Outlet, int>, IOutletRepository
    {
        public OutletRepository(IContext context)
            : base(context)
        { }
    }
}
