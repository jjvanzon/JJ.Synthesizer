using JetBrains.Annotations;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class ScaleTypeRepository : RepositoryBase<ScaleType, int>, IScaleTypeRepository
	{
		public ScaleTypeRepository(IContext context)
			: base(context)
		{ }
	}
}
