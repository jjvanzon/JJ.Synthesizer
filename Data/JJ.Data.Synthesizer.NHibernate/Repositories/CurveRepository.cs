using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class CurveRepository : JJ.Data.Synthesizer.DefaultRepositories.CurveRepository
    {
        public CurveRepository(IContext context)
            : base(context)
        { }
    }
}
