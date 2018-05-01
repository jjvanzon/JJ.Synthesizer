using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class CurveRepository : RepositoryBase<Curve, int>, ICurveRepository
	{
		// ReSharper disable once MemberCanBeProtected.Global
		public CurveRepository(IContext context)
			: base(context)
		{ }
	}
}
