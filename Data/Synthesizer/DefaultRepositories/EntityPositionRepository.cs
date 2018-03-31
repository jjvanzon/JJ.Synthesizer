using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class EntityPositionRepository : RepositoryBase<EntityPosition, int>, IEntityPositionRepository
	{
		public EntityPositionRepository(IContext context)
			: base(context)
		{ }
	}
}
