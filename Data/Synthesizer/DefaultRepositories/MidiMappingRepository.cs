using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class MidiMappingRepository : RepositoryBase<MidiMapping, int>, IMidiMappingRepository
	{
		public MidiMappingRepository(IContext context)
			: base(context)
		{ }
	}
}
