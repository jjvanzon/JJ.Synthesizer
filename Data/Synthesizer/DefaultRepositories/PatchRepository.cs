using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class PatchRepository : RepositoryBase<Patch, int>, IPatchRepository
    {
        public PatchRepository(IContext context)
            : base(context)
        { }
    }
}
