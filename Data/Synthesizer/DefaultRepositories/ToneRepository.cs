using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class ToneRepository : RepositoryBase<Tone, int>, IToneRepository
	{
		public ToneRepository(IContext context)
			: base(context)
		{ }
	}
}
