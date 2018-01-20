using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class MidiMappingElement_SideEffect_SetDefaults : ISideEffect
	{
		private readonly MidiMappingElement _entity;
		private readonly IDimensionRepository _dimensionRepository;

		public MidiMappingElement_SideEffect_SetDefaults(MidiMappingElement entity, IDimensionRepository dimensionRepository)
		{
			_entity = entity ?? throw new ArgumentNullException(nameof(entity));
			_dimensionRepository = dimensionRepository ?? throw new ArgumentNullException(nameof(dimensionRepository));
		}

		public void Execute()
		{
			_entity.IsActive = true;
			_entity.IsRelative = true;
			_entity.ControllerCode = 1; // Modulation wheel
			_entity.FromControllerValue = 0;
			_entity.TillControllerValue = 127;
			_entity.SetStandardDimensionEnum(DimensionEnum.VibratoDepth, _dimensionRepository);
			_entity.FromDimensionValue = 0;
			_entity.TillDimensionValue = 0.0005;
			_entity.MinDimensionValue = 0;
		}
	}
}
