using System;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Helpers
{
	public class MidiMappingRepositories
	{
		public IDimensionRepository DimensionRepository { get; }
		public IEntityPositionRepository EntityPositionRepository { get; }
		public IIDRepository IDRepository { get; }
		public IMidiMappingRepository MidiMappingRepository { get; }
		public IMidiMappingGroupRepository MidiMappingGroupRepository { get; }
		public IMidiMappingTypeRepository MidiMappingTypeRepository { get; }

		public MidiMappingRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new ArgumentNullException(nameof(repositoryWrapper));

			DimensionRepository = repositoryWrapper.DimensionRepository;
			EntityPositionRepository = repositoryWrapper.EntityPositionRepository;
			IDRepository = repositoryWrapper.IDRepository;
			MidiMappingRepository = repositoryWrapper.MidiMappingRepository;
			MidiMappingGroupRepository = repositoryWrapper.MidiMappingGroupRepository;
			MidiMappingTypeRepository = repositoryWrapper.MidiMappingTypeRepository;
		}
	}
}
