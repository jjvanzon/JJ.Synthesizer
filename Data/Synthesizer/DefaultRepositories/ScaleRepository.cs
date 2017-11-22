using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class ScaleRepository : RepositoryBase<Scale, int>, IScaleRepository
	{
		public ScaleRepository(IContext context)
			: base(context)
		{ }
	}
}
