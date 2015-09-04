using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class PatchRepository : RepositoryBase<Patch, int>, IPatchRepository
    {
        public PatchRepository(IContext context)
            : base(context)
        { }
    }
}
