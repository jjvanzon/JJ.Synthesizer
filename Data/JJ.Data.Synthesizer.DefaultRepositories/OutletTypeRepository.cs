using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OutletTypeRepository : RepositoryBase<OutletType, int>, IOutletTypeRepository
    {
        public OutletTypeRepository(IContext context)
            : base(context)
        { }
    }
}
