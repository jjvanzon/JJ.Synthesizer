using System;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Helpers
{
	public class MidiMappingElementRepositories
	{
		public IDimensionRepository DimensionRepository { get; }
		public IEntityPositionRepository EntityPositionRepository { get; }
		public IIDRepository IDRepository { get; }
		public IMidiMappingElementRepository MidiMappingElementRepository { get; }
		public IMidiMappingGroupRepository MidiMappingGroupRepository { get; }
		public IScaleRepository ScaleRepository { get; }

		public MidiMappingElementRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new ArgumentNullException(nameof(repositoryWrapper));

			DimensionRepository = repositoryWrapper.DimensionRepository;
			EntityPositionRepository = repositoryWrapper.EntityPositionRepository;
			IDRepository = repositoryWrapper.IDRepository;
			MidiMappingElementRepository = repositoryWrapper.MidiMappingElementRepository;
			MidiMappingGroupRepository = repositoryWrapper.MidiMappingGroupRepository;
			ScaleRepository = repositoryWrapper.ScaleRepository;
		}
	}
}
