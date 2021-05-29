using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class MidiMappingGroup_SideEffect_AutoCreate_MidiMapping : ISideEffect
    {
        private readonly MidiMappingGroup _midiMapping;
        private readonly MidiMappingFacade _midiMappingFacade;

        public MidiMappingGroup_SideEffect_AutoCreate_MidiMapping(MidiMappingGroup midiMapping, MidiMappingFacade midiMappingFacade)
        {
            _midiMapping = midiMapping ?? throw new ArgumentNullException(nameof(midiMapping));
            _midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
        }

        public void Execute()
        {
            bool mustExecute = _midiMapping.MidiMappings.Count == 0;
            if (mustExecute)
            {
                _midiMappingFacade.CreateMidiMappingWithDefaults(_midiMapping);
            }
        }
    }
}