using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Aggregates;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class PatchRepository : RepositoryBase<Patch, int>, IPatchRepository
	{
		public PatchRepository(IContext context)
			: base(context)
		{ }

		public Patch GetByName(string name)
		{
			Patch patch = TryGetByName(name);

			if (patch == null)
			{
				throw new NotFoundException<Patch>(new { name });
			}

			return patch;
		}

		public virtual Patch TryGetByName(string name)
		{
			throw new RepositoryMethodNotImplementedException();
		}
	}
}
