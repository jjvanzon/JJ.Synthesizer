using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class MidiMappingGroupRepository : RepositoryBase<MidiMappingGroup, int>, IMidiMappingGroupRepository
	{
		public MidiMappingGroupRepository(IContext context)
			: base(context)
		{ }
	}
}
