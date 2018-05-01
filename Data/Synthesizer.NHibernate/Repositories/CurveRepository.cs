using JetBrains.Annotations;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
	[UsedImplicitly]
	public class CurveRepository : DefaultRepositories.CurveRepository
	{
		public CurveRepository(IContext context)
			: base(context) { }
	}
}