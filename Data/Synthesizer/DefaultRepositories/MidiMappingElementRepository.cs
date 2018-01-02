using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class MidiMappingElementRepository : RepositoryBase<MidiMappingElement, int>, IMidiMappingElementRepository
	{
		public MidiMappingElementRepository(IContext context)
			: base(context)
		{ }
	}
}
