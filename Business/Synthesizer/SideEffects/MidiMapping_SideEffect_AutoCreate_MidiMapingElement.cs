using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class MidiMapping_SideEffect_AutoCreate_MidiMapingElement : ISideEffect
	{
		private readonly MidiMapping _midiMapping;
		private readonly MidiMappingFacade _midiMappingFacade;

		public MidiMapping_SideEffect_AutoCreate_MidiMapingElement(MidiMapping midiMapping, MidiMappingFacade midiMappingFacade)
		{
			_midiMapping = midiMapping ?? throw new ArgumentNullException(nameof(midiMapping));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		public void Execute()
		{
			bool mustExecute = _midiMapping.MidiMappingElements.Count == 0;
			if (mustExecute)
			{
				_midiMappingFacade.CreateMidiMappingElementWithDefaults(_midiMapping);
			}
		}
	}
}