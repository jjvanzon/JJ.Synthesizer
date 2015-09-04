using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class PatchRepository : JJ.Data.Synthesizer.DefaultRepositories.PatchRepository
    {
        public PatchRepository(IContext context)
            : base(context)
        { }
    }
}
