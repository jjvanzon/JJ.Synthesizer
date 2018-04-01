using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class MidiMappingTypeRepository : RepositoryBase<MidiMappingType, int>, IMidiMappingTypeRepository
	{
		public MidiMappingTypeRepository(IContext context)
			: base(context)
		{ }
	}
}
