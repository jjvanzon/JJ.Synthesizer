using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class MidiMapping_SideEffect_SetDefaults : ISideEffect
    {
        private readonly MidiMapping _entity;
        private readonly IDimensionRepository _dimensionRepository;
        private readonly IMidiMappingTypeRepository _midiMappingTypeRepository;

        public MidiMapping_SideEffect_SetDefaults(MidiMapping entity, IDimensionRepository dimensionRepository, [NotNull] IMidiMappingTypeRepository midiMappingTypeRepository)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _dimensionRepository = dimensionRepository ?? throw new ArgumentNullException(nameof(dimensionRepository));
            _midiMappingTypeRepository = midiMappingTypeRepository ?? throw new ArgumentNullException(nameof(midiMappingTypeRepository));
        }

        public void Execute()
        {
            _entity.IsActive = true;
            _entity.IsRelative = true;

            _entity.SetMidiMappingTypeEnum(MidiMappingTypeEnum.MidiController, _midiMappingTypeRepository);
            _entity.MidiControllerCode = 1; // Modulation wheel
            _entity.FromMidiValue = 0;
            _entity.TillMidiValue = 127;

            _entity.SetDimensionEnum(DimensionEnum.VibratoDepth, _dimensionRepository);
            _entity.FromDimensionValue = 0;
            _entity.TillDimensionValue = 0.0005;
            _entity.MinDimensionValue = 0;
        }
    }
}
